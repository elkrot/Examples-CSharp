// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace DocumentProtectionExcel
{
    /// <summary>
    /// Пользовательский элемент управления для панели действий, позволяющий
    /// управлять данными, отображенными в документе.
    /// </summary>
    public partial class TechniqueUserControl : UserControl
    {
        /// <summary>
        /// Выполните инициализацию компонентов и привязку dataDataGridView к источнику данных.
        /// </summary>
        public TechniqueUserControl()
        {
            InitializeComponent();
            // Выполняет привязку элемента управления dataDataGridView к источнику данных.
            this.dataDataGridView.DataSource = Globals.ThisWorkbook.custBindingSource;
        }

        /// <summary>
        /// Записывает значение в элемент управление dateDateTimePicker, записывает значение в
        /// dateTextBox только для чтения, изменяя его свойство ReadOnly на False, задавая 
        /// свойство Text, а позднее восстанавливая значение свойства.
        /// </summary>
        /// <param name="sender">Не используется.</param>
        /// <param name="e">Не используется.</param>
        private void dateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Записывает значение dateDateTimePicker в dateNamedRange 
                Globals.Sheet1.dateNamedRange.Value2 = dateDateTimePicker.Value;

                try
                {
                    // Снимает защиту со значения TextBox на Sheet1.
                    Globals.Sheet1.dateTextBox.ReadOnly = false;
                    // Записывает значение dateDateTimePicker в dateTextBox в ShortDatePattern.
                    Globals.Sheet1.dateTextBox.Text = dateDateTimePicker.Value.ToString("d", DateTimeFormatInfo.CurrentInfo);
                }
                finally
                {
                    // Защищает значение TextBox на Sheet1.
                    Globals.Sheet1.dateTextBox.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                        "Error changing dateNamedRange or dateTextBox value.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }

        }
    }
}
