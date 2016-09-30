// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataAnalysisExcel
{
    public partial class Sheet1
    {
        /// <summary>
        /// Это место, где будет создана сводная таблица.
        /// </summary>        
        private const string pivotTableAddress = "$B$22";

        /// <summary>
        /// Источник данных для списка продаж. Это представление основано на таблице "Продажи" объекта 
        /// Globals.DataSet, отфильтрованной для отображения данных по одному дню.
        /// </summary>
        private OperationsData.OperationsView dayView;

        /// <summary>
        /// Сводная таблица со статистикой продаж.
        /// </summary>
        private Excel.PivotTable pivotTable;

        /// <summary>
        /// Если текущая выбранная дата является последней датой, по которой есть данные,
        /// отображаются два дополнительных столбца: "Оцененный запас" и "Рекомендация",
        /// а свойство columnsAdded установлено в значение True. Иначе оно равно False.
        /// </summary>
        private bool columnsAdded;

        private void Sheet1_Startup(object sender, System.EventArgs e)
        {

            try
            {
                this.Sheet1_TitleLabel.Value2 = Properties.Resources.Sheet1Title;
                this.Name = Properties.Resources.Sheet1Name;

                this.newDateButton.Text = Properties.Resources.AddNewDateButton;
                this.saveButton.Text = Properties.Resources.SaveDataButton;
   
                this.dayView = Globals.DataSet.CreateView();

                if (Globals.DataSet.Sales.Count != 0)
                {
                    this.DateSelector.MinDate = Globals.DataSet.MinDate;
                    this.DateSelector.MaxDate = Globals.DataSet.MaxDate;
                    this.DateSelector.Value = this.DateSelector.MaxDate;
                }

                using (TextFileGenerator textFile = new TextFileGenerator(Globals.DataSet.Sales))
                {
                    this.pivotTable = CreatePivotTable(textFile.FullPath);
                }

                Globals.DataSet.Sales.SalesRowChanged += new OperationsBaseData.SalesRowChangeEventHandler(Sales_SalesRowChanged);
                UnscheduledOrderControl smartPaneControl = new UnscheduledOrderControl();
                smartPaneControl.Dock = DockStyle.Fill;
                smartPaneControl.View = this.dayView;

                Globals.ThisWorkbook.ActionsPane.Controls.Add(smartPaneControl);

                this.Activate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void Sheet1_Shutdown(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Обработчик событий ValueChanged для элемента DateTimePicker. Изменяет
        /// элемент dateView для отображения выбранной даты.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void DateSelector_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                DateTimePicker control = (DateTimePicker)sender;

                dayView.Date = control.Value;

                DateTime lastDay = control.MaxDate;

                if (control.Value == lastDay)
                {
                    AddColumns();
                }
                else
                {
                    RemoveColumns();
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Добавьте столбцы "Оцененный запас" и "Рекомендация" в объект списка
        /// на рабочем листе.
        /// </summary>
        private void AddColumns()
        {
            if (!columnsAdded)
            {
                dayView.BindToProtectedList(this.DayInventory, "Flavor", "Inventory", "Sold", "Profit", "Estimated Inventory", "Recommendation");

                SetLocalizedColumnNames();
                columnsAdded = true;
            }
        }

        /// <summary>
        /// Удалите столбцы "Оцененный запас" и "Рекомендация" из объекта списка
        /// на рабочем листе.
        /// </summary>
        private void RemoveColumns()
        {
            if (columnsAdded)
            {
                dayView.BindToProtectedList(this.DayInventory, "Flavor", "Inventory", "Sold", "Profit");
                SetLocalizedColumnNames();
                columnsAdded = false;
            }
        }

        /// <summary>
        /// Создайте сводную таблицу с данными из файла с текстом, разделенным табуляцией.
        /// </summary>
        /// <param name="filePath">Расположение текстового файла.</param>
        /// <returns>Созданная сводная таблица.</returns>
        private Excel.PivotTable CreatePivotTable(string filePath)
        {
            // Если таблица уже здесь,
            // возвращается существующая таблица.
            string tableName = Properties.Resources.AveragesPivotTableName;            
            Excel.PivotTables tables = (Excel.PivotTables)this.PivotTables(missing);
            System.Collections.Generic.Queue<double> savedWidths = new System.Collections.Generic.Queue<double>();
            
            if (tables != null)
            { 
                int count = tables.Count;

                for (int i = 1; i <= count; i++)
                {
                    Excel.PivotTable table = tables.Item(i);

                    if (table.Name == tableName)
                    {
                        return table;
                    }
                }
            }
            
            
            try
            {
                // Метод AddFields изменит размер столбцов. Сохраните ширину столбцов
                // для восстановления после добавления полей сводной таблицы
                foreach (Excel.Range column in DayInventory.HeaderRowRange.Cells)
                {
                    savedWidths.Enqueue((double)column.ColumnWidth);
                }
                
                // При создании сводной таблицы защита должна быть выключена.
                Globals.ThisWorkbook.MakeReadWrite();
               
                Excel.PivotTable table = Globals.ThisWorkbook.CreateSalesPivotTable(this.get_Range(pivotTableAddress, missing), filePath);
                table.Name = tableName;

                // Добавляет нужные строки и столбцы в 
                // сводную таблицу.
                table.AddFields("Flavor", missing, missing, missing);
                
                Excel.PivotField soldField = table.AddDataField(table.PivotFields("Sold"), Properties.Resources.AverageSold, Excel.XlConsolidationFunction.xlAverage);

                // Задание нужного представления данных в сводной таблице.
                // Формат "0,0" – один десятичный знак после запятой.
                soldField.NumberFormat = "0.0";

                Excel.PivotField profitField = table.AddDataField(table.PivotFields("Profit"), Properties.Resources.AverageProfit, Excel.XlConsolidationFunction.xlAverage);

                // Задание нужного представления данных в сводной таблице.
                // Формат "0,00" – два десятичных знака после запятой.
                profitField.NumberFormat = "0.00";

                // Скрытие двух плавающих панелей, которые добавляются при создании сводной таблицы.
                Globals.ThisWorkbook.ShowPivotTableFieldList = false;
                Globals.ThisWorkbook.Application.CommandBars["PivotTable"].Visible = false;

                return table;
            }
            finally
            {
                // Метод AddFields изменяет размер столбцов. Восстановите ширину столбцов.
                foreach (Excel.Range column in DayInventory.HeaderRowRange.Cells)
                {
                    column.ColumnWidth = savedWidths.Dequeue();
                }
                Globals.ThisWorkbook.MakeReadOnly();
            }
        }

        /// <summary>
        /// Задайте заголовки столбцов объекта списка на основе значений из таблицы ресурсов.
        /// </summary>
        private void SetLocalizedColumnNames()
        {
            string[] localizedInventoryColumns = {
                Properties.Resources.IceCreamHeader, 
                Properties.Resources.EodInventoryHeader, 
                Properties.Resources.UnitsSoldHeader, 
                Properties.Resources.NetGainHeader, 
                Properties.Resources.EstimatedInventoryHeader, 
                Properties.Resources.RecommendationHeader
            };

            try
            {
                Globals.ThisWorkbook.MakeReadWrite();
                this.DayInventory.HeaderRowRange.Value2 = localizedInventoryColumns;
            }
            finally
            {
                Globals.ThisWorkbook.MakeReadOnly();
            }
        }

        /// <summary>
        /// Выберите обработчик событий для кнопки newDateButton. Вставляет новые строки в таблицу "Продажи" для
        /// новой даты и задает объект списка для записи новой даты.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void newDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Globals.DataSet.IsLastDayComplete())
                {
                    MessageBox.Show(Globals.ThisWorkbook.IncompleteDataMessage);
                    return;
                }

                DateTime nextDate = Globals.DataSet.MaxDate.AddDays(1);

                foreach (OperationsBaseData.PricingRow row in Globals.DataSet.Pricing)
                {
                    OperationsBaseData.SalesRow newRow = (OperationsBaseData.SalesRow)this.dayView.Table.NewRow();
                    newRow.Flavor = row.Flavor;
                    newRow.Date = nextDate;
                    this.dayView.Table.AddSalesRow(newRow);
                }

                this.DateSelector.MaxDate = nextDate;
                this.DateSelector.Value = nextDate;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Выберите обработчик событий для кнопки saveButton. Записывает данные в файлы XML.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            Globals.DataSet.Save();
        }

        void Sales_SalesRowChanged(object sender, OperationsBaseData.SalesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Change)
            {
                Globals.ThisWorkbook.UpdateSalesPivotTable(this.pivotTable);
            }
        }

        #region Код, созданный конструктором VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.DateSelector.ValueChanged += new System.EventHandler(this.DateSelector_ValueChanged);
            this.newDateButton.Click += new System.EventHandler(this.newDateButton_Click);
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            this.Shutdown += new System.EventHandler(this.Sheet1_Shutdown);
            this.Startup += new System.EventHandler(this.Sheet1_Startup);

        }

        #endregion

    }
}
