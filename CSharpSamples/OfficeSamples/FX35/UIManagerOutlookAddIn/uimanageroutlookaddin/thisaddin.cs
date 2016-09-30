// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections;
using System.Collections.Generic;
using Office = Microsoft.Office;
using UiManagerOutlookAddIn.AddinUtilities;

namespace UiManagerOutlookAddIn
{
    // Этот класс несовместим с CLS из-за параметров своего базового класса.
    [CLSCompliant(false)]
    public partial class ThisAddIn
    {

        #region Стандартные операции

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion


        #region Поля

        internal TaskPaneConnector _taskPaneConnector;
        internal RibbonConnector _ribbonConnector;
        private FormRegionConnector _formRegionConnector;
        private Outlook.Inspectors _inspectors;
        private const String _controlProgId = "UiManager.SimpleControl";
        private const String _controlTitle = "SimpleControl";
        internal UserInterfaceElements _uiElements;

        #endregion


        #region Службы запросов

        // RequestService переопределяется, чтобы возвращать подходящий объект для каждого
        // нового реализуемого интерфейса расширяемости.
        protected override object RequestService(Guid serviceGuid)
        {
            if (serviceGuid ==
                typeof(Office.Core.ICustomTaskPaneConsumer).GUID)
            {
                if (_taskPaneConnector == null)
                {
                    _taskPaneConnector = new TaskPaneConnector();
                }
                return _taskPaneConnector;
            }

            else if (serviceGuid ==
                typeof(Office.Core.IRibbonExtensibility).GUID)
            {
                if (_ribbonConnector == null)
                {
                    _ribbonConnector = new RibbonConnector();
                }
                return _ribbonConnector;
            }

            else if (serviceGuid ==
                typeof(Outlook.FormRegionStartup).GUID)
            {
                if (_formRegionConnector == null)
                {
                    _formRegionConnector = new FormRegionConnector();
                }
                return _formRegionConnector;
            }

            return base.RequestService(serviceGuid);
        }

        #endregion


        #region Запуск

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            try
            {
                // Выполните инициализацию коллекции элементов пользовательского интерфейса и присоедините 
                // событие NewInspector, чтобы можно было добавлять каждый инспектор к 
                // коллекции по мере ее создания.
                _uiElements = new UserInterfaceElements();
                _inspectors = this.Application.Inspectors;
                _inspectors.NewInspector +=
                    new Outlook.InspectorsEvents_NewInspectorEventHandler(
                    inspectors_NewInspector);
            }
            catch (COMException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        #endregion


        #region NewInspector

        // Когда откроется новый инспектор, создайте панель задач и прикрепите 
        // ее к этому инспектору.
        void inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            CreateTaskPane(Inspector);
        }

        // Это поведение преобразовывается в общедоступный метод, таким образом 
        // его можно вызывать независимо от события NewInspector.
        // Это делается для того, чтобы разрешить условие состязания, когда обратные вызовы
        // ленты приходят перед присоединением события
        // NewInspector.
        public Office.Core.CustomTaskPane CreateTaskPane(
            Outlook.Inspector inspector)
        {
            Office.Core.CustomTaskPane taskpane = null;

            try
            {
                // Создайте новую панель задач, ее длина должна совпадать с 
                // шириной SimpleControl.
                taskpane = _taskPaneConnector.CreateTaskPane(
                        _controlProgId, _controlTitle, inspector);
                if (taskpane != null)
                {
                    
                    taskpane.Width = 230;

                    // Сопоставьте панель задач с инспектором и выполните ее кэширование 
                    // в коллекции.
                    _uiElements.Add(new UserInterfaceContainer(
                        inspector, taskpane, _ribbonConnector));
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return taskpane;
        }


        #endregion

    }
}
