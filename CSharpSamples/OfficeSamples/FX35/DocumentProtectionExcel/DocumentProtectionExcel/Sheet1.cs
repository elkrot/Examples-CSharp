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
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace DocumentProtectionExcel
{
    public partial class Sheet1
    {
        #region Свойства
        /// <summary>
        /// Возвращает, защищен ли лист.
        /// </summary>
        /// <value>
        /// Возвращает true, если лист защищен,
        /// или false, если не защищен.
        /// </value>
        internal bool IsProtected
        {
            get
            {
                return this.ProtectContents;
            }
        }
        #endregion

        #region Методы
        /// <summary>
        /// Защищает лист без пароля.
        /// </summary>
        internal void ProtectSheet()
        {
            // Подтверждает, что лист еще не защищен.
            if (this.IsProtected)
                throw new InvalidOperationException();

            // Защищает лист, так что его можно прочесть и 
            // снять с него защиту только без пароля.
            this.Protect(missing, missing, missing, missing,
                        missing, missing, missing, missing,
                        missing, missing, missing, missing,
                        missing, missing, missing, missing);
        }

        /// <summary>
        /// Снимает с листа защиту без пароля.
        /// </summary>
        internal void UnprotectSheet()
        {
            // Подтверждает, что с листа еще не снята защита.
            if (!this.IsProtected)
                throw new InvalidOperationException();

            // Снимает с листа защиту без пароля.
            this.Unprotect(missing);
        }

        #endregion

        /// <summary>
        /// Обрабатывает событие Startup для листа. Когда событие произойдет, 
        /// сначала будет снята защита с листа, затем customerListObject будет 
        /// привязан к customerBindingSource; лист будет защищен 
        /// позднее.
        /// </summary>
        /// <param name="sender">Не используется.</param>
        /// <param name="e">Не используется.</param>
        private void Sheet1_Startup(object sender, System.EventArgs e)
        {
            try
            {
                // Снимает защиту листа
                this.UnprotectSheet();

                // Создает ListObject и привязку к BindingSource.
                customerListObject.AutoSetDataBoundColumnHeaders = true;
                customerListObject.SetDataBinding(Globals.ThisWorkbook.custBindingSource, "", "firstName", "lastName", "userName");
            }
            finally
            {
                // Устанавливает защиту листа
                this.ProtectSheet();
            }
        }

        private void Sheet1_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Код, созданный конструктором VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet1_Startup);
            this.Shutdown += new System.EventHandler(Sheet1_Shutdown);
        }

        #endregion

    }
}
