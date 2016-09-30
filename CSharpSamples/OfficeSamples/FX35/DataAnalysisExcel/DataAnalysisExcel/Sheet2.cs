// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataAnalysisExcel
{
    /// <summary>
    /// На этом листе отображены результаты продаж за прошлые годы для сорта мороженого.
    /// </summary>
    public partial class Sheet2
    {
        /// <summary>
        /// Представление данных, построенное на основе таблицы "Продажи" с фильтрацией по сортам.
        /// </summary>
        private OperationsData.OperationsView view;

        /// <summary>
        /// Сорт, для которого отображается история продаж.
        /// </summary>
        private string flavor = null;

        /// <summary>
        /// Метод доступа и метод изменения для поля сорта. Когда
        /// свойство изменяется, соответственно изменяется представление.
        /// </summary>
        /// <value>Текущий сорт.</value>
        public string Flavor
        {
            get
            {
                return flavor;
            }
            set
            {
                flavor = value;

                if (FlavorChanged != null)
                {
                    FlavorChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Flavor"));
                }

                if (view != null)
                {
                    view.Flavor = flavor;
                }
            }
        }

        /// <summary>
        /// Событие, вызываемое при изменении Flavor. Если свойство Flavor используется
        /// для привязки данных, PropertyManager прослушивает это событие.
        /// </summary>
        public event EventHandler FlavorChanged;

        private void Sheet2_Startup(object sender, System.EventArgs e)
        {
            this.Sheet2_TitleLabel.Value2 = Properties.Resources.Sheet2Title;
            this.Name = Properties.Resources.Sheet2Name;
            this.IceCreamLabel.Value2 = Properties.Resources.IceCreamHeader;
            
            this.Chart_1.ChartTitle.Text = Properties.Resources.ProfitHeader;
            ((Excel.Axis)this.Chart_1.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = Properties.Resources.ProfitHeader;
            ((Excel.Axis)this.Chart_1.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = Properties.Resources.DateHeader;

            this.view = Globals.DataSet.CreateView();

            if (this.Flavor != null)
            {
                view.Flavor = this.Flavor;
            }
            else if (view.Count != 0)
            {
                this.Flavor = (string)view[0].Row["Flavor"];
            }

            this.FlavorNamedRange.DataBindings.Add("Value2", this, "Flavor");

            this.History.SetDataBinding(view, "", "Date", "Inventory", "Sold", "Profit");

            this.History.ListColumns[1].Name = Properties.Resources.DateHeader;
            this.History.ListColumns[2].Name = Properties.Resources.InventoryHeader;
            this.History.ListColumns[3].Name = Properties.Resources.SoldHeader;
            this.History.ListColumns[4].Name = Properties.Resources.ProfitHeader;            
        }

        private void Sheet2_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Код, созданный конструктором VSTO

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet2_Startup);
            this.Shutdown += new System.EventHandler(Sheet2_Shutdown);
        }

        #endregion

    }
}
