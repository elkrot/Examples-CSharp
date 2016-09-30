// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace DataContentControls
{
    public partial class ThisDocument
    {
        private ActionsPaneControl1 myActionsPane = new ActionsPaneControl1();
        private void ThisDocument_Startup(object sender, System.EventArgs e)
        {
            // TODO: удалите эту строку кода, чтобы удалить AutoFill по умолчанию для "northwindDataSet.Employees".
            if (this.NeedsFill("northwindDataSet"))
            {
                this.employeesTableAdapter.Fill(this.northwindDataSet.Employees);
           }
            this.ActionsPane.Controls.Add(myActionsPane); 
        }

        private void ThisDocument_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Код, созданный конструктором VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Shutdown += new System.EventHandler(this.ThisDocument_Shutdown);
            this.Startup += new System.EventHandler(this.ThisDocument_Startup);

        }

        #endregion
    }
}
