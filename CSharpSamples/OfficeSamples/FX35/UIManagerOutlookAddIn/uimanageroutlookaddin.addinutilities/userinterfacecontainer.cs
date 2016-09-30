// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office;

namespace UiManagerOutlookAddIn.AddinUtilities
{
    public class UserInterfaceContainer
    {
        
        #region Содержащиеся элементы пользовательского интерфейса

        private Outlook.InspectorEvents_10_Event _inspectorEvents;
        private Outlook.Inspector _inspector;
        private IFormRegionControls _formRegionControls;
        private IRibbonConnector _ribbonConnector;
        private Office.Core.CustomTaskPane _taskPane;
        private bool _isControlVisible;

        public event EventHandler InspectorClose;

        [CLSCompliant(false)]
        public Outlook.Inspector Inspector
        {
            get { return _inspector; }
            set { _inspector = value; }
        }

        [CLSCompliant(false)]
        public Office.Core.CustomTaskPane TaskPane
        {
            get { return _taskPane; }
            set { _taskPane = value; }
        }

        [CLSCompliant(false)]
        public IFormRegionControls FormRegionControls
        {
            get { return _formRegionControls; }
            set { _formRegionControls = value; }
        }


        [CLSCompliant(false)]
        public IRibbonConnector RibbonConnector
        {
            get { return _ribbonConnector; }
            set { _ribbonConnector = value; }
        }

        #endregion


        #region Методы доступа элемента управления "лента"

        public bool IsControlVisible
        {
            get { return _isControlVisible; }
        }

        public void ShowRibbonControl(String ribbonControlId)
        {
            _isControlVisible = true;
            _ribbonConnector.Ribbon.InvalidateControl(ribbonControlId);
        }

        public void HideRibbonControl(String ribbonControlId)
        {
            _isControlVisible = false;
            _ribbonConnector.Ribbon.InvalidateControl(ribbonControlId);
        }

        #endregion


        #region ctor

        // Этот метод не является CLS-совместимым из-за его параметров Office.
        [CLSCompliant(false)]
        public UserInterfaceContainer(
            Outlook.Inspector inspector, 
            Office.Core.CustomTaskPane taskPane, 
            IRibbonConnector ribbonConnector)
        {
            if (inspector != null)
            {
                _inspector = inspector;
                _taskPane = taskPane;
                _ribbonConnector = ribbonConnector;

                // Скройте событие InspectorClose, чтобы можно было выполнить очистку.
                _inspectorEvents = (Outlook.InspectorEvents_10_Event)_inspector;
                _inspectorEvents.Close +=
                    new Outlook.InspectorEvents_10_CloseEventHandler(
                    _inspectorEvents_Close);
            }
        }

        #endregion


        #region Событие инспектора Close

        void _inspectorEvents_Close()
        {
            // Выйдите из коллекции объектов пользовательского интерфейса, 
            // отсоедините событие close и выполните очистку ссылок.
            _inspectorEvents.Close -=
                new Outlook.InspectorEvents_10_CloseEventHandler(
                _inspectorEvents_Close);

            if (InspectorClose != null)
            {
                // Скажите всем, что мы закрываемся.
                InspectorClose(this, new EventArgs());
            }

            _inspector = null;
            _inspectorEvents = null;
            _taskPane = null;
            _formRegionControls = null;
            _ribbonConnector = null;
        }

        #endregion

    }
}
