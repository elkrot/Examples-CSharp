// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office;

namespace UiManagerOutlookAddIn
{
    public class TaskPaneConnector : Office.Core.ICustomTaskPaneConsumer
    {
        private Office.Core.ICTPFactory _ctpFactory;

        internal Microsoft.Office.Core.CustomTaskPane CreateTaskPane(string id, string title, object parentWindow)
        {
            if (_ctpFactory != null)
                return _ctpFactory.CreateCTP(id, title, parentWindow);

            return null;
        }
        
        
        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public void CTPFactoryAvailable(
            Office.Core.ICTPFactory CTPFactoryInst)
        {
            // Здесь нужно только кэшировать объект CTPFactory, 
            // таким образом, позднее можно будет создать пользовательские панели задач.
            _ctpFactory = CTPFactoryInst;
        }
    }
}
