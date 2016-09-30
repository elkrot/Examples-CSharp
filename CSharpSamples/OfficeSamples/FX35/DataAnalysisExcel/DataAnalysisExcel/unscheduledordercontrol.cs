// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Globalization;

namespace DataAnalysisExcel
{
    /// <summary>
    /// Это элемент управления, отображающийся в области действий. Он дает возможность создавать
    /// незапланированный заказ на мороженое и просматривать журнал продаж мороженого.
    /// </summary>
    public partial class UnscheduledOrderControl : UserControl
    {
        /// <summary>
        /// Затраты на размещение незапланированного заказа.
        /// </summary>
        const double unscheduledDeliveryCost = 25;

        /// <summary>
        /// Возвращает или устанавливает представление текущего дня.
        /// </summary>
        /// <value></value>
        internal OperationsData.OperationsView View
        {
            get
            {
                return this.view;
            }
            set
            {
                if (this.view != null)
                {
                    this.view.ListChanged -= new ListChangedEventHandler(view_ListChanged);
                }

                this.view = value;

                if (this.view != null)
                {
                    this.view.ListChanged += new ListChangedEventHandler(view_ListChanged);
                    UpdateRecommendationLabel();
                }
            }
        }

        public UnscheduledOrderControl()
        {
            this.InitializeComponent();

            System.ComponentModel.ComponentResourceManager resources = 
                new System.ComponentModel.ComponentResourceManager(typeof(UnscheduledOrderControl));
            resources.ApplyResources(this.selectorLabel, "selectorLabel", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.flavorComboBox, "flavorComboBox", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.highLabel, "highLabel", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.lowLabel, "lowLabel", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.highList, "highList", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.lowList, "lowList", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.recommendationGroup, "recommendationGroup", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.createOrderButton, "createOrderButton", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.orderLabel, "orderLabel", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this.recommendationLabel, "recommendationLabel", CultureInfo.CurrentUICulture);
            resources.ApplyResources(this, "$this", CultureInfo.CurrentUICulture);

            // Данные привязывают поле со списком сортов к таблице прейскуранта.
            this.flavorComboBox.DataSource = Globals.DataSet.Pricing;
            this.flavorComboBox.DisplayMember = "Flavor";
        }

        /// <summary>
        /// Создает новый лист заказа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateOrderButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                new OrderingSheet(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик событий для события SelectedIndexChanged поля со списком сортов.
        /// Сделайте так, чтобы на листе журнала сортов отображался выбранный сорт.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flavorComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataRowView selectedItem = (DataRowView)((ComboBox)sender).SelectedItem;
            DisplayFlavorHistory(((OperationsBaseData.PricingRow)selectedItem.Row).Flavor);
        }

        /// <summary>
        /// Обработчик событий для события ListChanged представления. Если представление изменяется, чтобы отобразить
        /// не самую последнюю дату, единственными отображаемыми элементами являются
        /// поле со списком и его надпись. Если изменяются данные запаса в представлении, обновляются три элемента управления:
        /// список недостаточного запаса, список достаточного запаса и подпись рекомендации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (this.View.Date == Globals.DataSet.MaxDate)
            {
                if (e.ListChangedType == ListChangedType.Reset)
                {
                    ShowLastDayControls(true);
                }
                else if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    double estimatedInventory = (double)this.View[e.NewIndex]["Estimated Inventory"];
                    string flavor = (string)this.View[e.NewIndex]["Flavor"];

                    if (estimatedInventory < 0)
                    {
                        ShowAsLowInventory(flavor);
                    }
                    else
                    {
                        double todaysInventory;
                        todaysInventory = ((OperationsBaseData.SalesRow)this.view[e.NewIndex].Row).Inventory;

                        double idealInventory;
                        idealInventory = todaysInventory - estimatedInventory;

                        // если более чем на 10 процентов больше обоснованного предыдущими продажами,
                        if (todaysInventory > idealInventory * 1.1)
                        {
                            ShowAsHighInventory(flavor);
                        }
                        else
                        {
                            ShowAsAdequateInventory(flavor);
                        }
                    }

                    UpdateRecommendationLabel();
                }
            }
            else
            {
                if (e.ListChangedType == ListChangedType.Reset)
                {
                    ShowLastDayControls(false);
                }
            }
        }

        /// <summary>
        /// Отображает сорт как позицию с недостаточным запасом.
        /// </summary>
        /// <param name="flavor">Отображаемый сорт.</param>
        void ShowAsLowInventory(string flavor)
        {
            if (!this.lowList.Items.Contains(flavor))
            {
                this.lowList.Items.Add(flavor);

                if (this.highList.Items.Contains(flavor))
                {
                    this.highList.Items.Remove(flavor);
                }
            }
        }

        /// <summary>
        /// Отображает сорт как позицию с достаточным запасом.
        /// </summary>
        /// <param name="flavor">Отображаемый сорт.</param>
        void ShowAsHighInventory(string flavor)
        {
            if (!this.highList.Items.Contains(flavor))
            {
                this.highList.Items.Add(flavor);

                if (this.lowList.Items.Contains(flavor))
                {
                    this.lowList.Items.Remove(flavor);
                }
            }
        }

        /// <summary>
        /// Удаляет сорт из списков достаточных и недостаточных запасов.
        /// </summary>
        /// <param name="flavor">Сорт для удаления из списков.</param>
        void ShowAsAdequateInventory(string flavor)
        {
            if (this.highList.Items.Contains(flavor))
            {
                this.highList.Items.Remove(flavor);
            }

            if (this.lowList.Items.Contains(flavor))
            {
                this.lowList.Items.Remove(flavor);
            }
        }

        /// <summary>
        /// Отобразите или скройте списки достаточных и недостаточных запасов, 
        /// их подписи и группу рекомендаций.
        /// </summary>
        /// <param name="show">True – чтобы отобразить, False – чтобы скрыть.</param>
        private void ShowLastDayControls(bool show)
        {
            Control[] dynamicControls = new Control[] {
                this.highLabel, this.highList, this.lowLabel, this.lowList, this.recommendationGroup
            };

            if (show)
            {
                foreach (Control c in dynamicControls)
                {
                    c.Show();
                }
            }
            else
            {
                foreach (Control c in dynamicControls)
                {
                    c.Hide();
                }
            }
        }

        /// <summary>
        /// Щелкните обработчик событий в списках достаточного и недостаточного запаса. Отображается журнал продаж
        /// по выбранному сорту.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventoryList_Click(object sender, EventArgs e)
        {
            string flavor = (string)(((ListBox)sender).SelectedItem);
            if (flavor != null)
            {
                DisplayFlavorHistory(flavor);
            }
        }

        /// <summary>
        /// Отображается журнал продаж по сортам на листе "Журнал сортов".
        /// </summary>
        /// <param name="flavor">Сорт, по которому отображается журнал.</param>
        private static void DisplayFlavorHistory(string flavor)
        {
            Globals.Sheet2.Flavor = flavor;
            Globals.Sheet2.Activate();
        }

        /// <summary>
        /// Вычисляет потенциальную прибыль от заказа мороженого.
        /// </summary>
        /// <returns>Потенциальная прибыль.</returns>
        private double CalculatePotentialProfit()
        {
            double profit = 0 - unscheduledDeliveryCost;

            foreach (DataRowView rowView in this.view)
            {
                OperationsBaseData.SalesRow row = (OperationsBaseData.SalesRow)rowView.Row;

                if (!row.IsEstimated_InventoryNull() && row.Estimated_Inventory < 0)
                {
                    OperationsBaseData.PricingRow pricing = (OperationsBaseData.PricingRow)row.GetParentRow("Pricing_Sales");
                    double flavorProfit = (pricing.Price - pricing.Cost) * (0 - row.Estimated_Inventory);

                    profit += flavorProfit;
                }
            }

            return profit;
        }

        /// <summary>
        /// Вычисляет и отображает рекомендации по заказу в recommendationLabel.
        /// </summary>
        void UpdateRecommendationLabel()
        {
            double profit = CalculatePotentialProfit();

            if (profit > 0)
            {
                this.recommendationLabel.Text = string.Format(
                    CultureInfo.CurrentUICulture,
                    Properties.Resources.UnscheduledOrderRecommended, 
                    profit);
            }
            else
            {
                this.recommendationLabel.Text = string.Format(
                    CultureInfo.CurrentUICulture,
                    Properties.Resources.UnscheduledOrderNotRecommended, 
                    0 - profit);
            }
        }
    }
}
