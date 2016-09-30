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
    public partial class Sheet2
    {
        /// <summary>
        /// Строка для хранения исходного значения в usernameNamedRange 
        /// элемента управления.
        /// </summary>
        private string userName;

        /// <summary>
        /// Обрабатывает событие Startup для листа. Когда событие произойдет, 
        /// значение элемента управления usernameNamedRange будет получено
        /// и сохранено в поле userName.
        /// </summary>
        /// <param name="sender">Не используется.</param>
        /// <param name="e">Не используется.</param>
        private void Sheet2_Startup(object sender, System.EventArgs e)
        {
            // Получает начальное значение userNameNamedRange
            userName = this.usernameNamedRange.Value2.ToString();
        }

        private void Sheet2_Shutdown(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Обрабатывает событие изменения NamedRange. Когда это событие произойдет, 
        /// исходное значение элемента управления NamedRange будет восстановлено, а также 
        /// будет отображено окно сообщения, указывающее, что пользователь не 
        /// имеет полномочий на изменение значения.
        /// </summary>
        /// <param name="sender">Не используется.</param>
        /// <param name="e">Не используется.</param>
        private void usernameNamedRange_Change(Microsoft.Office.Interop.Excel.Range Target)
        {
            try
            {
                // Отключает обработчик события 
                this.usernameNamedRange.Change -= new Microsoft.Office.Interop.Excel.DocEvents_ChangeEventHandler(this.usernameNamedRange_Change);
                this.usernameNamedRange.Value2 = userName;

                MessageBox.Show("You are not authorized to change this value.",
                    "Document Protection Techniques - Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error handling NamedRange change event.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                // Включает обработчик события
                this.usernameNamedRange.Change += new Microsoft.Office.Interop.Excel.DocEvents_ChangeEventHandler(this.usernameNamedRange_Change);
            }

        }


        #region Код, созданный конструктором VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.usernameNamedRange.Change += new Microsoft.Office.Interop.Excel.DocEvents_ChangeEventHandler(this.usernameNamedRange_Change);
            this.Startup += new System.EventHandler(Sheet2_Startup);
            this.Shutdown += new System.EventHandler(Sheet2_Shutdown);
        }

        #endregion

    }
}
