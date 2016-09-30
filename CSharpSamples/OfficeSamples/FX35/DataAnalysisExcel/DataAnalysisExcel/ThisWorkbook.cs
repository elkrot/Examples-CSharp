// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Globalization;

namespace DataAnalysisExcel
{
    public partial class ThisWorkbook
    {

        public string IncompleteDataMessage
        {
            get
            {
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    Properties.Resources.DataIncompleteError,
                    Globals.DataSet.MaxDate);

                return message;
            }
        }

        /// <summary>
        /// Меню "Заказы".
        /// </summary>
        private Office.CommandBarPopup menuBar;

        /// <summary>
        /// Панель инструментов "Заказы".
        /// </summary>
        private Office.CommandBar toolBar;

        /// <summary>
        /// 
        /// </summary>
        private Office.CommandBarButton weeklyToolbarButton;

        private Office.CommandBarButton unscheduledToolbarButton;

        private Office.CommandBarButton weeklyMenuItem;

        private Office.CommandBarButton unscheduledMenuItem;

        private const string salesPivotTableConnectionTemplate = "ODBC;DBQ={0};DefaultDir={0};Driver={{Microsoft Text Driver (*.txt; *.csv)}};DriverId=27;Extensions=*;FIL=text;MaxBufferSize=2048;MaxScanRows=25;PageTimeout=5;SafeTransactions=0;Threads=3;UID=admin;UserCommitSync=Yes;";

        private const string salesPivotTableQueryTemplate = "SELECT [Date], Flavor, Sold, Profit FROM {0}";
        

        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            AddToolbar();
            AddMenu();

            this.Deactivate += new Microsoft.Office.Interop.Excel.WorkbookEvents_DeactivateEventHandler(ThisWorkbook_Deactivate);
            this.ActivateEvent += new Microsoft.Office.Interop.Excel.WorkbookEvents_ActivateEventHandler(ThisWorkbook_ActivateEvent);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Создает лист с заданным именем. Если лист с этим именем
        /// существует, вызывается исключение ApplicationException.
        /// </summary>
        /// <param name="name">Имя нового листа.</param>
        /// <returns>Новый лист.</returns>
        internal Excel.Worksheet CreateWorksheet(string name)
        {
            Excel.Sheets sheetCollection = Globals.ThisWorkbook.Worksheets;

            foreach (object item in sheetCollection)
            {
                if (ExcelHelpers.GetName(item) == name)
                {
                    throw new ArgumentException(
                        String.Format(
                            CultureInfo.CurrentUICulture, 
                            Properties.Resources.WorksheetExistsError, 
                            name), 
                        "name");
                }
            }

            object after;

            if (sheetCollection.Count != 0)
            {
                after = sheetCollection[sheetCollection.Count];
            }
            else
            {
                after = missing;
            }

            Excel.Worksheet sheet;
            sheet = (Excel.Worksheet)sheetCollection.Add(missing, after, missing, missing);
            sheet.Name = name;

            return sheet;
        }

        internal static string GetAbsolutePath(string relativePath)
        {
            string assembyUrl = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            string assemblyPath = System.IO.Path.GetDirectoryName(new System.Uri(assembyUrl).LocalPath);
            return System.IO.Path.GetFullPath(System.IO.Path.Combine(assemblyPath, relativePath));
        }

        void AddMenu()
        {
            string menuCaption = Properties.Resources.OrdersMenu;
            string weeklyCaption = Properties.Resources.WeeklyMenu;
            string unscheduledCaption = Properties.Resources.UnscheduledMenu;

            Office.CommandBar mainMenuBar = this.ThisApplication.CommandBars.ActiveMenuBar;

            Office.CommandBarPopup cmdBarControl = null;
            Office.CommandBarButton weeklyButton;
            Office.CommandBarButton unscheduledButton;

            // Панели команд являются общими для всего приложения.
            // Если есть две рабочие книги, в которых выполняется этот код,
            // сначала нужно проверить, имеются ли во второй рабочей книге
            // панели инструментов, чтобы меню
            // не было добавлено дважды.
            foreach (Office.CommandBarControl currentControl in mainMenuBar.Controls)
            {
                if (currentControl.Caption == menuCaption)
                {
                    cmdBarControl = (Office.CommandBarPopup)currentControl;
                    break;
                }
            }

            if (cmdBarControl == null)
            {
                cmdBarControl = (Office.CommandBarPopup)mainMenuBar.Controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, missing, true);
                cmdBarControl.Caption = menuCaption;
                cmdBarControl.Tag = menuCaption;
            }
            
            if (cmdBarControl.CommandBar.FindControl(missing, missing, weeklyCaption, missing, true) == null)
            {
                weeklyButton = (Office.CommandBarButton)cmdBarControl.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
                weeklyButton.Caption = weeklyCaption;
                weeklyButton.Tag = weeklyCaption;
            }
            else
            {
                weeklyButton = (Office.CommandBarButton)cmdBarControl.Controls[weeklyCaption];
            }

            if (cmdBarControl.CommandBar.FindControl(missing, missing, unscheduledCaption, missing, true) == null)
            {
                unscheduledButton = (Office.CommandBarButton)cmdBarControl.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
                unscheduledButton.Caption = unscheduledCaption;
                unscheduledButton.Tag = unscheduledCaption;
            }
            else
            {
                unscheduledButton = (Office.CommandBarButton)cmdBarControl.Controls[unscheduledCaption];
            }

            weeklyButton.Click += delegate
            {
                try
                {
                    new OrderingSheet(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };

            unscheduledButton.Click += delegate
            {
                try
                {
                    new OrderingSheet(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };

            // Необходимо присвоить кнопки переменным-членам,
            // чтобы они не были "очищены" сборщиком мусора.
            this.weeklyMenuItem = (Office.CommandBarButton)weeklyButton;
            this.unscheduledMenuItem = (Office.CommandBarButton)unscheduledButton;

            this.menuBar = cmdBarControl;
        }

        private void AddToolbar()
        {
            string barName = Properties.Resources.OrdersToolbar;
            string weeklyCaption = Properties.Resources.WeeklyMenu;
            string unscheduledCaption = Properties.Resources.UnscheduledMenu;
            
            // Панели инструментов уникальны для всего приложения.
            // Если есть две рабочие книги, в которых выполняется этот код,
            // сначала нужно проверить, имеются ли во второй рабочей книге
            // панели инструментов.
            // Если ее нет, ее нужно создать.
            // В любом случае требуется добавить обработчик.
            Office.CommandBar commandBar = null;

            Office.CommandBarButton weeklyButton;
            Office.CommandBarButton unscheduledButton;

            for (int i = 1; i <= ThisApplication.CommandBars.Count; ++i)
            {
                if (ThisApplication.CommandBars[i].Name == barName)
                {
                    commandBar = ThisApplication.CommandBars[i];
                    break;
                }
            }

            if (commandBar == null)
            {
                commandBar = this.Application.CommandBars.Add(barName, Office.MsoBarPosition.msoBarTop, missing, true);
            }


            if (commandBar.FindControl(missing, missing, weeklyCaption, missing, false) == null)
            {
                weeklyButton = (Office.CommandBarButton)commandBar.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, missing);

                weeklyButton.Caption = weeklyCaption;

                weeklyButton.Picture = ExcelHelpers.Convert(toolBarImages.Images["CreateWeeklyOrder"]);
                weeklyButton.Mask = ExcelHelpers.Convert(toolBarImages.Images["CreateWeeklyOrderMask"]);
                weeklyButton.Tag = weeklyCaption;
            }
            else
            {
                weeklyButton = (Office.CommandBarButton)commandBar.Controls[weeklyCaption];
            }

            if (commandBar.FindControl(missing, missing, unscheduledCaption, missing, false) == null)
            {
                unscheduledButton = (Office.CommandBarButton)commandBar.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, missing);

                unscheduledButton.Caption = unscheduledCaption;

                unscheduledButton.Picture = ExcelHelpers.Convert(this.toolBarImages.Images["CreateUnscheduledOrder"]);
                unscheduledButton.Mask = ExcelHelpers.Convert(this.toolBarImages.Images["CreateUnscheduledOrderMask"]);
                unscheduledButton.Tag = unscheduledCaption;
            }
            else
            {
                unscheduledButton = (Office.CommandBarButton)commandBar.Controls[unscheduledCaption];
            }

            commandBar.Visible = true;

            this.weeklyToolbarButton = weeklyButton;
            this.unscheduledToolbarButton = unscheduledButton;

            this.toolBar = commandBar;
        }

        private void ThisWorkbook_Deactivate()
        {
            this.menuBar.Visible = false;
            this.toolBar.Visible = false;
        }

        private void ThisWorkbook_ActivateEvent()
        {
            this.menuBar.Visible = true;
            this.toolBar.Visible = true;
        }

        private void ThisWorkbook_BeforeSave(bool SaveAsUI, ref bool Cancel)
        {
            Globals.DataSet.Save();
        }

        internal Excel.PivotTable CreateSalesPivotTable(Excel.Range range, String filePath)
        {
            string fileDirectory = System.IO.Path.GetDirectoryName(filePath);
            string fileName = System.IO.Path.GetFileName(filePath);

            string pivotTableName = Properties.Resources.SalesAndProfitPivotTableName;
            Excel.PivotCache cache = this.PivotCaches().Add(Excel.XlPivotTableSourceType.xlExternal, missing);

            cache.Connection = String.Format(
                CultureInfo.CurrentUICulture, 
                salesPivotTableConnectionTemplate, 
                fileDirectory);
            cache.CommandType = Excel.XlCmdType.xlCmdSql;
            cache.CommandText = String.Format(
                CultureInfo.CurrentUICulture, 
                salesPivotTableQueryTemplate, 
                fileName);

            Excel.PivotTable pivotTable = cache.CreatePivotTable(range, pivotTableName, missing, Excel.XlPivotTableVersionList.xlPivotTableVersionCurrent);

            // Настраивает свойства новой сводной таблицы для
            // форматирования сведений нужным образом.
            pivotTable.ErrorString = " -- ";
            pivotTable.DisplayErrorString = true;
            pivotTable.NullString = " -- ";

            return pivotTable;
        }

        /// <summary>
        /// Экспортирует данные из объекта списка в текстовый файл и обновляет сводную таблицу
        /// с помощью этих данных.
        /// </summary>
        internal void UpdateSalesPivotTable(Excel.PivotTable pivotTable)
        {
            bool screenUpdating = Globals.ThisWorkbook.Application.ScreenUpdating;
            Application.ScreenUpdating = false;

            TextFileGenerator generator = new TextFileGenerator(Globals.DataSet.Sales);

            try
            {
                string fileDirectory = System.IO.Path.GetDirectoryName(generator.FullPath);
                string fileName = System.IO.Path.GetFileName(generator.FullPath);

                this.ShowPivotTableFieldList = false;

                pivotTable.SourceData = new string[] { 
                    String.Format(
                    CultureInfo.CurrentUICulture, 
                    salesPivotTableConnectionTemplate, 
                    fileDirectory), 

                    String.Format(
                    CultureInfo.CurrentUICulture, 
                    salesPivotTableQueryTemplate, 
                    fileName) 
                };

                this.Application.CommandBars["PivotTable"].Visible = false;
            }
            finally
            {
                generator.Dispose();
                this.Application.ScreenUpdating = screenUpdating;
            }
        }

        internal void MakeReadOnly()
        {
            Globals.Sheet1.Protect(
                "",                  // Пароль
                true,  //  DrawingObjects
                true,  //  Содержание
                true,  //  Сценарии
                false, //  UserInterfaceOnly
                true,  //  AllowFormattingCells
                true,  //  AllowFormattingColumns
                true,  //  AllowFormattingRows
                false, //  AllowInsertingColumns
                false, //  AllowInsertingRows
                false, //  AllowInsertingHyperlinks
                false, //  AllowDeletingColumns
                false, //  AllowDeletingRows
                true,  //  AllowSorting
                true,  //  AllowFiltering
                true); //  AllowUsingPivotTables
        }

        internal void MakeReadWrite()
        {
            Globals.Sheet1.Unprotect("");
            
            // В некоторых сценариях даже после вызова Unprotect()
            // не удается выполнить SetDataBinding. Назначение этого выделения решает
            // проблему.
            Excel.Range selection = (Excel.Range)this.Application.Selection;
            if (selection != null)
                selection.Select();
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

    partial class Globals
    {
        private static OperationsData ds;


        internal static OperationsData DataSet
        {
            get
            {
                if (ds == null)
                {
                    ds = new OperationsData();
                    ds.Load();
                }

                return ds;
            }
        }
    }
}
