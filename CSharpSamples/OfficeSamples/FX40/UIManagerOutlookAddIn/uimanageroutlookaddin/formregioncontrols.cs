// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Microsoft.Vbe.Interop.Forms;
using UiManagerOutlookAddIn.AddinUtilities;

namespace UiManagerOutlookAddIn
{
    // Класс FormRegionControls использует оболочку для ссылок на элементы управления в
    // пользовательской области формы. Создается новый экземпляр этого класса для
    // каждой открываемой пользовательской области формы. Таким образом можно убедиться, что любой
    // ответ пользовательского интерфейса будет уникален для этого экземпляра (то есть, когда пользователь нажимает
    // командную кнопку, можно получить текст из текстового поля для нужного экземпляра.
    public class FormRegionControls : IFormRegionControls
    {

        #region Поля

        private UserForm _form;
        private Outlook.Inspector _inspector;
        private Outlook.OlkListBox _coffeeList;
        private Microsoft.Vbe.Interop.Forms.TextBox _ordersText;
        private String[] _listItems = 
            {"Colombia Supremo", "Ethiopia Longberry Harrar", 
            "Sumatra Mandehling", "Mocha Java" , "Jamaica Blue Mountain", 
            "Costa Rica Tarrazu", "Monsooned Malabar" };
        private const String _formRegionListBoxName = "coffeeListBox";
        private const String _ordersTextBoxName = "ordersTextBox";
        private const String _mailServiceGroup = "mailServiceGroup";

        [CLSCompliant(false)]
        public Outlook.Inspector Inspector
        {
            get { return _inspector; }
            set { _inspector = value; }
        }

        #endregion


        // Этот метод не является CLS-совместимым из-за его входного параметра.
        [CLSCompliant(false)]
        public FormRegionControls(Outlook.FormRegion region)
        {
            if (region != null)
            {
                try
                {
                    // Выполните кэширование ссылки на эту область, ее 
                    // инспектора, а также располагающихся на этой области элементов управления.
                    _inspector = region.Inspector;
                    _form = region.Form as UserForm;
                    _ordersText = _form.Controls.Item(_ordersTextBoxName) 
                        as Microsoft.Vbe.Interop.Forms.TextBox;
                    _coffeeList = _form.Controls.Item(_formRegionListBoxName) 
                        as Outlook.OlkListBox;

                    // Заполните поле со списком произвольными строками.
                    for (int i = 0; i < _listItems.Length; i++)
                    {
                        _coffeeList.AddItem(_listItems[i], i);
                    }
                    _coffeeList.Change += new
                        Outlook.OlkListBoxEvents_ChangeEventHandler(
                        _coffeeList_Change);
                }
                catch (COMException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }


        // Пользователь изменил выбранный фрагмент в поле со списком в пользовательской 
        // области формы.
        private void _coffeeList_Change()
        {
            UserInterfaceContainer uiContainer = 
                Globals.ThisAddIn._uiElements.GetUIContainerForInspector(
                _inspector);

            // Сделайте видимыми кнопки служебной ленты надстроек.
            uiContainer.ShowRibbonControl(_mailServiceGroup);

            // Получите пользовательский элемент управления на панели задач и скопируйте текст 
            // элемента, выбранного в поле со списком на области формы, в поле со списком
            // области задач.
            SimpleControl sc = 
                (SimpleControl)uiContainer.TaskPane.ContentControl;
            sc._coffeeGroup.Visible = true;
            sc._coffeeList.Items.Add(_coffeeList.Text);
        }


        // Задайте текстовое значение порядков TextBox в области формы.
        public void SetControlText(String controlName, String text)
        {
            switch (controlName)
            {
                case _ordersTextBoxName :
                    _ordersText.Text = text;
                    break;
            }
        }

    }
}
