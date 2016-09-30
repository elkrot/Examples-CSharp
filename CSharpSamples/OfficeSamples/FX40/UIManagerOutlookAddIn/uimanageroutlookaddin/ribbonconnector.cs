// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office;
using System.Globalization;
using UiManagerOutlookAddIn.AddinUtilities;

namespace UiManagerOutlookAddIn
{

    [ComVisible(true)]
    [Guid("66299133-E2CC-46c1-8C47-B73DA7203067")]
    [ProgId("UiManager.RibbonConnector")]
    public class RibbonConnector : 
        Office.Core.IRibbonExtensibility, IRibbonConnector 
    {

        #region Стандартные операции

        public RibbonConnector()
        {
        }

        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public void OnLoad(Office.Core.IRibbonUI ribbonUI)
        {
            this._ribbon = ribbonUI;
        }

        public string GetCustomUI(string RibbonID)
        {
            string xml = null;

            // Существуют две различные настройки ленты: одна для инспекторов
            // заданий, и вторая для всех остальных инспекторов.
            switch (RibbonID)
            {
                case _taskItemName:
                    xml = Properties.Resources.TaskRibbon;
                    break;
                default:
                    xml = Properties.Resources.SimpleRibbon;
                    break;
            }
            return xml;
        }

        // XML-лента указывает, что группа addinServiceButtons, которая
        // содержит элементы управления sendList, является условно видимой. Этот 
        // метод несовместим с CLS, так как имеет параметр Office.
        [CLSCompliant(false)]
        public bool GetVisible(Office.Core.IRibbonControl control)
        {
            if (control == null)
            {
                return false;
            }

            // Сопоставьте этот экземпляр элемента управления (определенный инспектором) в
            // коллекции, а затем верните текущее значение кэшированного 
            // состояния видимости.
            Outlook.Inspector inspector = control.Context as Outlook.Inspector;
            UserInterfaceContainer uiContainer =
                Globals.ThisAddIn._uiElements.GetUIContainerForInspector(inspector);
            return uiContainer.IsControlVisible;
        }

        #endregion

        #region Поля

        private Office.Core.IRibbonUI _ribbon;


        private const String _taskItemName = "Microsoft.Outlook.Task";
        private const String _mailAddressee = "someone@example.com";
        private const String _orderName = "Coffee";
        private const String _ordersTextBoxName = "ordersTextBox";
        private static int _orderCount;

        #endregion


        #region OnToggleTaskPane

        // Этот метод не является CLS-совместимым из-за его входного параметра.
        [CLSCompliant(false)]
        public void OnToggleTaskPane(
            Office.Core.IRibbonControl control, bool isPressed)
        {
            // Включите видимость пользовательской панели задач.
            if (control != null)
            {
                // Найдите инспектор для данной ленты, таким образом можно будет найти 
                // соответствующую панель задач из коллекции.
                Outlook.Inspector inspector = 
                    (Outlook.Inspector)control.Context;
                Office.Core.CustomTaskPane taskpane =
                    Globals.ThisAddIn._uiElements.GetTaskPaneForInspector(
                    inspector);

                // Если вызов произошел раньше возможности добавить данный 
                // инспектор или область задач в коллекцию, можно сделать это сейчас.
                if (taskpane == null)
                {
                    taskpane = Globals.ThisAddIn.CreateTaskPane(inspector);
                }

                taskpane.Visible = isPressed;
            }
        }

        #endregion


        #region Обработчик кнопки

        private String GetTextFromTaskPane(Outlook.Inspector inspector)
        {
            String coffeeText = null;

            if (inspector != null)
            {
                // Получите пользовательский элемент управления в области задач.
                Office.Core.CustomTaskPane taskpane =
                    Globals.ThisAddIn._uiElements.GetTaskPaneForInspector(inspector);
                SimpleControl sc = (SimpleControl)taskpane.ContentControl;

                // Составьте тело сообщения электронной почты из строк в поле со списком области задач.
                StringBuilder builder = new StringBuilder();
                foreach (string s in sc._coffeeList.Items)
                {
                    builder.AppendLine(s);
                }
                coffeeText = builder.ToString();
            }

            return coffeeText;
        }

        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public void OnSendList(Office.Core.IRibbonControl control)
        {
            if (control != null)
            {
                try
                {
                    Outlook.Inspector inspector =
                        (Outlook.Inspector)control.Context;
                    String coffeeText = GetTextFromTaskPane(inspector);

                    // Создайте новое сообщение электронной почты из входных параметров, а затем отправьте его.
                    Outlook._MailItem mi =
                        (Outlook._MailItem)
                        Globals.ThisAddIn.Application.CreateItem(
                        Outlook.OlItemType.olMailItem);
                    mi.Subject = _orderName;
                    mi.Body = coffeeText;
                    mi.To = _mailAddressee;
                    mi.Send();

                    // Обновите счетчик порядков в области формы.
                    UserInterfaceContainer uiContainer =
                        Globals.ThisAddIn._uiElements.GetUIContainerForInspector(
                        inspector);
                    CultureInfo cultureInfo = new CultureInfo("en-us");
                    uiContainer.FormRegionControls.SetControlText(
                        _ordersTextBoxName, (++_orderCount).ToString(cultureInfo));
                }
                catch (COMException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }

        #endregion


        #region Члены IRibbonConnector

        [CLSCompliant(false)]
        public Office.Core.IRibbonUI Ribbon
        {
            get { return _ribbon; }
            set { _ribbon = value; }
        }

        #endregion
    }
}
