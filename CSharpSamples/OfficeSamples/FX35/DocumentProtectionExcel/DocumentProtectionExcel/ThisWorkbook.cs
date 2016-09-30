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
    public partial class ThisWorkbook
    {
        /// <summary>
        /// Элемент управления DataGridView на ActionsPane и элемент управления ListObject 
        /// на Sheet1 совместно используют один и тот же BindingSource. Когда значение в DataGridView 
        /// изменяется, значение ListObject изменяется соответственно. Однако, 
        /// поскольку Sheet1 защищен, понадобится снять защиту со Sheet1, чтобы
        /// изменить значение ListObject.
        /// </summary>
        internal class CustomerBindingSource : BindingSource
        {
            protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
            {
                try
                {
                    try
                    {
                        // Снимает защиту Sheet1
                        Globals.Sheet1.UnprotectSheet();
                        base.OnListChanged(e);
                    }
                    finally
                    {
                        // Устанавливает защиту Sheet1
                        Globals.Sheet1.ProtectSheet();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error protecting or unprotecting sheet",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        /// Пользовательский элемент управления, который будет добавлен на панель действий.
        /// </summary>
        private TechniqueUserControl techUserControl;
        /// <summary>
        /// Набор данных для хранения из xml-файла ExcelSampleData.
        /// </summary>
        internal DataSet customerDataSet = null;
        /// <summary>
        /// Источник привязки, использующийся для привязки данных к набору.
        /// </summary>
        internal CustomerBindingSource custBindingSource = null;

        #region Методы
        /// <summary>
        /// Загружает DataSet с данными из xml-файла ExcelSampleData.
        /// </summary>
        private void LoadDataSet()
        {
            try
            {
                if (customerDataSet == null)
                    customerDataSet = new DataSet();
                // Получает расположение файла схемы
                string schemaFileLocation = System.IO.Path.Combine(Path, "ExcelSampleData.xsd");
                // Получает расположение xml-файла
                string xmlFileLocation = System.IO.Path.Combine(Path, "ExcelSampleData.xml");

                // Считывает данные из файла схемы и xml-файла
                customerDataSet.ReadXmlSchema(schemaFileLocation);
                customerDataSet.ReadXml(xmlFileLocation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Error loading data set.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        /// <summary>
        /// Обрабатывает событие Startup для рабочей книги. Когда событие произойдет, 
        /// метод LoadDataSet будет вызван для того, чтобы загрузить данные из файла XML в
        /// customerDataSet, задать для свойства DataSource, принадлежащего customerBindingSource  
        /// для customerDataSet и его DataMember значение "Клиенты". Необходимо создать TechniqueUserControl
        /// и прикрепить его к ActionsPane.
        /// </summary>
        /// <param name="sender">Не используется.</param>
        /// <param name="e">Не используется.</param>
        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            // Загружает набор данных из файла xml
            LoadDataSet();

            // Создает BindingSource
            if (custBindingSource == null)
                custBindingSource = new CustomerBindingSource();
            custBindingSource.DataSource = customerDataSet;
            custBindingSource.DataMember = "Customer";

            // Добавление строк в таблицу данных не снимет защиту с листа, когда 
            // ListObject пытается изменить размер, который приведет к исключению. Задайте
            // свойству AllowNew значение false, чтобы принудительно сохранять размеры набора данных.
            custBindingSource.AllowNew = false;

            // Добавляет пользовательский элемент управления на панель действий.
            techUserControl = new TechniqueUserControl();
            ActionsPane.Controls.Add(techUserControl);
        }

        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
        }
        #region Код, созданный конструктором VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
        }

        #endregion

    }
}
