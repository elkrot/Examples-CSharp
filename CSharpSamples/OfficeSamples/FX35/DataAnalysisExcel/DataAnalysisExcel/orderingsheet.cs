// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace DataAnalysisExcel
{
    internal class OrderingSheet
    {
        object defaultParameter = System.Type.Missing;

        internal enum StatHeadings
        {
            DailySales = 0,
            Required,
            CurrentInventory,
            ProjectInventory,
            OrderQuanity
        }

        private string[] headers = {
            Properties.Resources.MaxDailySalesHeader, 
            Properties.Resources.ProjectedSalesHeader, 
            Properties.Resources.CurrentInventoryHeader,
            Properties.Resources.ProjectedInventoryHeader,
            Properties.Resources.OrderQuantityHeader
        };

        DateTime deliveryDate;

        DateTime nextScheduledDeliveryDate;

        DateTime orderDate;

        Excel.Worksheet worksheet;

        const string orderDateAddress = "$B$4";
        const string pivotTableAddress = "$B$10";

        internal OrderingSheet(bool isUnscheduled)
        {
            if (!Globals.DataSet.IsLastDayComplete())
            {
                throw new ApplicationException(Globals.ThisWorkbook.IncompleteDataMessage);
            }

            this.orderDate = Globals.DataSet.MaxDate;

            string worksheetName;

            if (isUnscheduled)
            {
                worksheetName = ExcelHelpers.CreateValidWorksheetName(
                    String.Format(
                        CultureInfo.CurrentUICulture, 
                        Properties.Resources.UnscheduledOrderSheetName,
                        this.orderDate.ToShortDateString()));
            }
            else
            {
                worksheetName = ExcelHelpers.CreateValidWorksheetName(
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        Properties.Resources.WeeklyOrderSheetName,
                        this.orderDate.ToShortDateString()));
            }
            Excel.Worksheet worksheet = null;

            // Создание листа вызовет исключение, если это имя уже существует.
            try
            {
                worksheet = Globals.ThisWorkbook.CreateWorksheet(worksheetName);
            }
            catch (Exception ex)
            {
                string message;

                if (isUnscheduled)
                {
                    message = String.Format(
                        CultureInfo.CurrentUICulture,
                        Properties.Resources.UnscheduledOrderSheetCreationError,
                        worksheetName);
                }
                else
                {
                    message = String.Format(
                        CultureInfo.CurrentUICulture,
                        Properties.Resources.WeeklyOrderSheetCreationError,
                        worksheetName);
                }

                throw new ApplicationException(message, ex);
            }

            this.worksheet = worksheet;

            CreateOrder(isUnscheduled);
        }

        internal OrderingSheet(Excel.Worksheet worksheet, DateTime orderDate, bool isUnscheduled)
        {
            this.orderDate = orderDate;
            this.worksheet = worksheet;

            CreateOrder(isUnscheduled);
        }

        private DateTime ComputeUnscheduledDeliveryDate()
        {
            return this.orderDate.AddDays(1).Date;
        }

        private DateTime ComputeWeeklyDeliveryDate()
        {
            return Globals.DataSet.NextWeeklyDeliveryDate;
        }

        private void CreateOrder(bool isUnscheduled)
        {
            if (isUnscheduled)
            {
                this.deliveryDate = ComputeUnscheduledDeliveryDate();
                this.nextScheduledDeliveryDate = ComputeWeeklyDeliveryDate();
            }
            else
            {
                this.deliveryDate = ComputeWeeklyDeliveryDate();
                this.nextScheduledDeliveryDate = this.deliveryDate.AddDays(7);
            }

            // Создается сводная таблица со сведениями относительно 
            // количества проданного мороженого.
            this.PopulateDateInformation(this.orderDate);

            Excel.PivotTable pivotTable = this.CreatePivotTable();

            this.AddCalculations(pivotTable);
        }

        private void AddCalculations(Excel.PivotTable pivotTable)
        {
            // Получает диапазоны для сводной таблицы.
            Excel.Range tableRange = pivotTable.TableRange1;
            Excel.Range dataRange = pivotTable.DataBodyRange;

            // Получает каждый столбец, который требуется добавить согласно
            // сводной таблице.
            System.Array values = Enum.GetValues(typeof(StatHeadings));

            // Определяет левую верхнюю ячейку для сводной таблицы.
            Excel.Range tableStartCell = ExcelHelpers.GetCellFromRange(tableRange, 1, 1);

            // Получает первую доступную ячейку в соответствующей строке в
            // конце сводной таблицы.
            Excel.Range nextHeader = tableStartCell.get_End(Excel.XlDirection.xlDown).get_End(Excel.XlDirection.xlToRight).get_End(Excel.XlDirection.xlUp).Next;

            // Определяет границу ячеек, составляющих вычисленные поля
            // для текущего столбца.
            Excel.Range colStart = ExcelHelpers.GetCellFromRange(nextHeader, 2, 1);

            Excel.Range colEnd = colStart.get_Offset(dataRange.Rows.Count - 1, 0);

            // Для каждого столбца, который требуется добавить,
            // заполняет его статистические данные и заголовки.

            foreach (int i in values)
            {
                nextHeader.Value2 = this.headers[i];
                this.PopulateStatColumn(i, colStart, colEnd);
                nextHeader = nextHeader.Next;
                colStart = colStart.Next;
                colEnd = colEnd.Next;
            }
        }

        private void PopulateStatColumn(int column, Excel.Range start, Excel.Range end)
        {
            try
            {
                // Определяет диапазон для заполнения данными.
                Excel.Range twoLines = start.get_Resize(2, 1);

                twoLines.Merge(System.Type.Missing);

                Excel.Range fillRange = this.worksheet.get_Range(start, end);
                end.Select();

                switch (column)
                {
                    case (int)StatHeadings.DailySales:
                        // Заполняет столбец ежедневных продаж.
                        // Получает адреса ячеек, содержащих
                        // стандартное отклонение и среднее значение.
                        Excel.Range average = start.Previous;
                        string averageAddress = average.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);
                        Excel.Range standardDev = average.get_Offset(1, 0);
                        string standardDevAddress = standardDev.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);

                        // Задает формулы для столбца.
                        start.Formula = "=" + averageAddress + "+ (2*" + standardDevAddress + ")";

                        // Формат "0,00" – два десятичных знака после запятой.
                        start.NumberFormat = "0.00";
                        twoLines.AutoFill(fillRange, Excel.XlAutoFillType.xlFillDefault);
                        break;

                    case (int)StatHeadings.Required:
                        // Заполняет требующийся столбец.
                        // Определяет адрес ячейки, содержащей
                        // ожидаемые продажи.
                        Excel.Range expectedSales = start.Previous;
                        string salesAddress = expectedSales.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);

                        // Определяет необходимый запас 
                        // до поставки.
                        // Определяет число дней до поставки.
                        int waitDays = this.GetDaysToDelivery();

                        start.Formula = "=" + waitDays + "*" + salesAddress;

                        // Формат "0,00" – два десятичных знака после запятой.
                        start.NumberFormat = "0.00";
                        twoLines.AutoFill(fillRange, Excel.XlAutoFillType.xlFillDefault);
                        break;

                    case (int)StatHeadings.CurrentInventory:
                        // Заполняет столбец текущего запаса.
                        // Получает из журнала диапазон на последний день.
                        int count = (end.Row - start.Row + 1) / 2;
                        Excel.Range currentCell = start;

                        for (int row = 0; row < count; row += 1)
                        {
                            Excel.Range flavorCell = currentCell.get_Offset(0, 0 - 5);


                            string flavor = ExcelHelpers.GetValueAsString(flavorCell);
                            int inventory = Globals.DataSet.Sales.FindByDateFlavor(Globals.DataSet.MaxDate, flavor).Inventory;

                            currentCell.Value2 = inventory;

                            if (row != 0)
                            {
                                Excel.Range twoCells = currentCell.get_Resize(2, 1);

                                twoCells.Merge(System.Type.Missing);
                                currentCell = twoCells;
                            }

                            currentCell = currentCell.get_Offset(1, 0);
                        }

                        break;

                    case (int)StatHeadings.ProjectInventory:

                        // Получает адреса для планируемых продаж и
                        // текущего запаса.
                        Excel.Range currentInventory = start.Previous;
                        Excel.Range required = currentInventory.Previous;
                        string currentInventoryAddress = currentInventory.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);
                        string requiredAddress = required.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);

                        // Определяет запас, ожидаемый на 
                        // дату поставки.
                        start.Formula = "=MAX(0," + currentInventoryAddress + "-" + requiredAddress + ")";

                        // Формат "0,00" – два десятичных знака после запятой.
                        start.NumberFormat = "0.00";
                        twoLines.AutoFill(fillRange, Excel.XlAutoFillType.xlFillDefault);
                        break;

                    case (int)StatHeadings.OrderQuanity:
                        // Определяет адреса для планируемого запаса
                        // и необходимого количества.
                        Excel.Range projectedInventory = start.Previous;
                        Excel.Range needed = projectedInventory.Previous.Previous;
                        string projectedInventoryAddress = projectedInventory.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);
                        string neededAddress = needed.get_Address(false, false, Excel.XlReferenceStyle.xlA1, defaultParameter, defaultParameter);

                        // Определяет необходимый объем заказа по каждой позиции.
                        start.Formula = "=" + neededAddress + "-" + projectedInventoryAddress;

                        // Формат "0,00" – два десятичных знака после запятой.
                        start.NumberFormat = "0.00";
                        twoLines.AutoFill(fillRange, Excel.XlAutoFillType.xlFillDefault);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private int GetDaysToDelivery()
        {
            // Этот метод определяет число дней
            // до следующей плановой поставки.
            // Это требуется для оценки срока заказа в днях.

            TimeSpan difference = this.nextScheduledDeliveryDate - this.deliveryDate;

            return difference.Days;
        }

        Excel.PivotTable CreatePivotTable()
        {
            TextFileGenerator generator = new TextFileGenerator(Globals.DataSet.Sales);

            try
            {
                Excel.Range destination = this.worksheet.get_Range(pivotTableAddress, defaultParameter);
                Excel.PivotTable pivotTable;

                pivotTable = Globals.ThisWorkbook.CreateSalesPivotTable(destination, generator.FullPath);

                // Настраивает свойства новой сводной таблицы 
                pivotTable.ColumnGrand = false;
                pivotTable.RowGrand = false;

                // Добавляет нужные строки и столбцы в 
                // сводную таблицу.
                pivotTable.AddFields("Flavor", defaultParameter, defaultParameter, defaultParameter);

                Excel.PivotField soldField = pivotTable.AddDataField(pivotTable.PivotFields("Sold"), Properties.Resources.AverageOfUnitsSold, Excel.XlConsolidationFunction.xlAverage);

                // Задание нужного представления данных в сводной таблице.
                // Формат "0,0" – один десятичный знак после запятой.
                soldField.NumberFormat = "0.0";

                Excel.PivotField profitField = pivotTable.AddDataField(pivotTable.PivotFields("Sold"), Properties.Resources.StdDevOfUnitsSold, Excel.XlConsolidationFunction.xlStDev);

                // Задание нужного представления данных в сводной таблице.
                // Формат "0,00" – два десятичных знака после запятой.
                profitField.NumberFormat = "0.00";

                // Скрытие двух плавающих панелей, которые добавляются при создании сводной таблицы.
                Globals.ThisWorkbook.ShowPivotTableFieldList = false;
                Globals.ThisWorkbook.Application.CommandBars["PivotTable"].Visible = false;

                return pivotTable;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                generator.Dispose();
            }

        }


        private void PopulateDateInformation(DateTime selectedDate)
        {
            // Этот метод заполняет лист следующей датой заказа 
            // и его соответствующей датой поставки.
            // Получает дату следующего заказа и заполняет его.
            Excel.Range orderDateCell = worksheet.get_Range(orderDateAddress, defaultParameter);

            orderDateCell.Value2 = Properties.Resources.OrderDateHeader;
            orderDateCell.Next.Value2 = selectedDate.ToShortDateString();

            Excel.Range deliveryDateCell = ExcelHelpers.GetCellFromRange(orderDateCell, 2, 1);

            deliveryDateCell.Value2 = Properties.Resources.DeliveryDateHeader;
            deliveryDateCell.Next.Value2 = this.deliveryDate.ToShortDateString();
        }

    }
}