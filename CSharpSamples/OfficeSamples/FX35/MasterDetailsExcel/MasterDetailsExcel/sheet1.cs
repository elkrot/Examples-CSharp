// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Diagnostics;

namespace MasterDetailsRelationships
{
    public partial class Sheet1
    {
        private string[] productListColumnHeaders = { "ProductName", "Quantity", "Inventory" };

        private const int productNameColumn = 1;
        private const int quantityOrderedColumn = 2;
        private const int currentInventoryColumn = 3;
        private const string quantityOrderedChartSeries = "=\"Quantity Ordered\"";
        private const string inventoryChartSeries = "=\"Inventory\"";
        private const string noOrderSelectedTitle = "No Order Selected";
        private const string canFulfillOrderTitle = "Order Can Be Fulfilled";
        private const string cannotFulfillOrderTitle = "Order Not Ready for Fulfillment";

        private void Sheet1_Startup(object sender, System.EventArgs e)
        {
            // Задайте заголовки столбцов для ProductList.
            this.ProductList.HeaderRowRange.Value2 = productListColumnHeaders;

            // Задайте заголовки для диаграммы.
            ((Excel.Series)this.OrdersChart.SeriesCollection(1)).Name = quantityOrderedChartSeries;
            ((Excel.Series)this.OrdersChart.SeriesCollection(2)).Name = inventoryChartSeries;
            this.OrdersChart.ChartTitle.Text = noOrderSelectedTitle;

            // Привяжите ProductList к данным выбранного в настоящее время заказа.
            this.ProductList.SetDataBinding(Globals.ThisWorkbook.OrderDetailsBindingSource,
                null, productListColumnHeaders);

            // Привяжите диапазон с названием "Статус" к статусу выбранного в настоящее время заказа.
            this.Status.DataBindings.Add("Value2", Globals.ThisWorkbook.StatusBindingSource, "Status");
        }

        void ProductList_Change(Microsoft.Office.Interop.Excel.Range targetRange, Microsoft.Office.Tools.Excel.ListRanges changedRanges)
        {
            this.UpdateChart();
        }

        /// <summary>
        /// Обновляет заголовок диаграммы на основе сведений о выбранном 
        /// в настоящее время заказе.
        /// </summary>
        private void UpdateChart()
        {
            if (Globals.ThisWorkbook.CustomerOrdersBindingSource.Count == 0)
                this.OrdersChart.ChartTitle.Text = noOrderSelectedTitle;
            else if (this.CanFulfillOrder())
                this.OrdersChart.ChartTitle.Text = canFulfillOrderTitle;
            else
                this.OrdersChart.ChartTitle.Text = cannotFulfillOrderTitle;
        }

        /// <summary>
        /// Определяет, имеются ли достаточные запасы продуктов 
        /// для выбранного в настоящее время заказа.
        /// </summary>
        /// <returns></returns>
        private bool CanFulfillOrder()
        {
            Excel.Range listRange = this.ProductList.DataBodyRange;

            for (int i = 1; i <= listRange.Rows.Count; i++)
            {
                // Возвращает значения в ListRow.
                object[,] values = (object[,])((Excel.Range)listRange.Rows[i, missing]).Value2;

                // Определите, какой продукт представляет строка.
                if (values[1, productNameColumn] == null)
                    continue;
                string product = values[1, productNameColumn].ToString();

                // Если в этой строке присутствует продукт, определите доступное количество этого продукта.
                if (!String.IsNullOrEmpty(product))
                {
                    int quantity = Convert.ToInt32(values[1, quantityOrderedColumn]);
                    CompanyData.ProductsRow productRow = Globals.ThisWorkbook.CurrentCompanyData.Products.FindByName(product);

                    // Проверьте, имеется ли достаточный заказ для заказанного количества.
                    if ((productRow.Inventory - quantity) < 0)
                        return false;
                }
            }

            return true;
        }

        private void Status_Change(Microsoft.Office.Interop.Excel.Range Target)
        {
            // Получите StatusID для статуса, установленного в диапазоне с названием "Статус".
            Debug.Assert((Globals.ThisWorkbook.CustomerOrdersBindingSource.Current as DataRowView) != null);
            DataRowView currentRow = (DataRowView)Globals.ThisWorkbook.CustomerOrdersBindingSource.Current;
            Debug.Assert((currentRow.Row as CompanyData.OrdersRow) != null);
            CompanyData.OrdersRow orderRow = (CompanyData.OrdersRow)currentRow.Row;
            int newStatus = Globals.ThisWorkbook.CurrentCompanyData.Status.FindByStatus(
                this.Status.Value2.ToString()).StatusID;

            // Проверьте, установлено ли для статуса значение "Выполнен", если в действительности
            // он не мог иметь такого значения. В этом случае предупредите пользователя, что заказ не может быть выполнен.
            if (newStatus == 0 && orderRow.StatusID !=0 && !this.CanFulfillOrder())
            {
                MessageBox.Show("Order cannot be fulfilled with current inventory levels.");
                this.Status.Value2 = orderRow.StatusRow.Status;
                return;
            }
            else if (newStatus == 0 && orderRow.StatusID != 0)
            {
                // Заказ был изменен на выполненный заказ, поэтому нужно
                // обновить запасы, чтобы удалить количество, которое было отправлено.
                this.UpdateInventory();
            }

            // Обновите заказ, чтобы отразить новый статус.
            orderRow.StatusID = newStatus;
        }

        /// <summary>
        /// Обновляет доступный запас продуктов на основании текущего выполненного заказа.
        /// </summary>
        private void UpdateInventory()
        {
            Excel.Range listRange = this.ProductList.DataBodyRange;

            for (int i = 1; i <= listRange.Rows.Count; i++)
            {
                // Возвращает значения в ListRow.
                object[,] values = (object[,])((Excel.Range)listRange.Rows[i, missing]).Value2;
                if (values[1, productNameColumn] == null)
                    continue;

                // Определите, какой продукт представляет строка.
                string product = values[1, productNameColumn].ToString();

                // Если в этой строке присутствует продукт, определите доступное количество этого продукта.
                if (!String.IsNullOrEmpty(product))
                {
                    int quantity = Convert.ToInt32(values[1, quantityOrderedColumn]);
                    CompanyData.ProductsRow productRow = Globals.ThisWorkbook.CurrentCompanyData.Products.FindByName(product);

                    // Обновите ProductRow, чтобы отразить новый уровень запаса.
                    productRow.Inventory = productRow.Inventory - quantity;
                }
            }

            // Сохраните изменения в наборе данных (DataSet).
            Globals.ThisWorkbook.CurrentCompanyData.AcceptChanges();
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
            this.Shutdown += new System.EventHandler(this.Sheet1_Shutdown);
            this.Startup += new System.EventHandler(this.Sheet1_Startup);
            this.ProductList.Change += new Microsoft.Office.Tools.Excel.ListObjectChangeHandler(ProductList_Change);
            this.Status.Change += new Microsoft.Office.Interop.Excel.DocEvents_ChangeEventHandler(Status_Change);
        }
        #endregion
    }
}
