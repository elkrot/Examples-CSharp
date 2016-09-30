// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace UiManagerOutlookAddIn.AddinUtilities
{
    public class UserInterfaceElements
    {

        #region Инициализация

        private List<UserInterfaceContainer> _items;

        public UserInterfaceElements()
        {
            _items = new List<UserInterfaceContainer>();
        }

        #endregion


        #region Обновления коллекции

        public void Add(UserInterfaceContainer uiContainer)
        {
            _items.Add(uiContainer);
            uiContainer.InspectorClose += 
                new EventHandler(uiContainer_InspectorClose);
        }

        // Когда закрывается инспектор, закрывается и область формы, так что нужно 
        // удалить этот экземпляр из коллекции.
        void uiContainer_InspectorClose(object sender, EventArgs e)
        {
            _items.Remove(sender as UserInterfaceContainer);
        }

        public bool Remove(UserInterfaceContainer uiContainer)
        {
            return _items.Remove(uiContainer);
        }

        // Обновите объект контейнера пользовательского интерфейса, обозначенный
        // данным инспектором, прикрепив данный объект FormRegionControls.
        // Этот метод не является CLS-совместимым из-за его параметров Office.
        [CLSCompliant(false)]
        public bool AttachFormRegion(
            Outlook.Inspector inspector, 
            IFormRegionControls formRegionControls)
        {
            bool updateOK = false;

            // Найдите этот инспектор в коллекции контейнеров.
            UserInterfaceContainer uiContainer = 
                GetUIContainerForInspector(inspector);
            if (uiContainer != null)
            {
                uiContainer.FormRegionControls = formRegionControls;
                updateOK = true;
            }
            return updateOK;
        }

        #endregion


        #region Поиск коллекций

        // Если дан инспектор, найдите подходящий объект контейнера пользовательского интерфейса.
        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public UserInterfaceContainer GetUIContainerForInspector(
            Outlook.Inspector inspector)
        {
            UserInterfaceContainer uiContainer = null;

            foreach (UserInterfaceContainer uic in _items)
            {
                if (uic.Inspector == inspector)
                {
                    uiContainer = uic;
                    break;
                }
            }
            return uiContainer;
        }

        // Если дан инспектор, верните подходящую панель задач.
        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public Office.Core.CustomTaskPane GetTaskPaneForInspector(
            Outlook.Inspector inspector)
        {
            Office.Core.CustomTaskPane taskpane = null;

            foreach (UserInterfaceContainer uic in _items)
            {
                if (uic.Inspector == inspector)
                {
                    taskpane = uic.TaskPane;
                    break;
                }
            }
            return taskpane;
        }

        // Если дан инспектор, верните подходящую ленту.
        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public IRibbonConnector GetRibbonForInspector(
            Outlook.Inspector inspector)
        {
            IRibbonConnector ribbonConnector = null;

            foreach (UserInterfaceContainer uic in _items)
            {
                if (uic.Inspector == inspector)
                {
                    ribbonConnector = uic.RibbonConnector;
                    break;
                }
            }
            return ribbonConnector;
        }

        // Если дан UserControl, верните подходящую ленту.
        // Этот метод не совместим с CLS из-за его типа возвращаемого значения.
        [CLSCompliant(false)]
        public IRibbonConnector GetRibbonForUserControl(
            UserControl userControl)
        {
            IRibbonConnector ribbonConnector = null;

            foreach (UserInterfaceContainer uic in _items)
            {
                if (uic.TaskPane.ContentControl == userControl)
                {
                    ribbonConnector = uic.RibbonConnector;
                    break;
                }
            }
            return ribbonConnector;
        }

        // Если дан UserControl, верните подходящий объект контейнера пользовательского интерфейса.
        public UserInterfaceContainer GetUIContainerForUserControl(
            UserControl userControl)
        {
            UserInterfaceContainer uiContainer = null;

            foreach (UserInterfaceContainer uic in _items)
            {
                if (uic.TaskPane.ContentControl == userControl)
                {
                    uiContainer = uic;
                    break;
                }
            }
            return uiContainer;
        }

        #endregion

    }
}
