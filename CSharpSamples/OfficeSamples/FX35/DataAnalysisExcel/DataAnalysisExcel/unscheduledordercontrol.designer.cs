namespace DataAnalysisExcel
{
    partial class UnscheduledOrderControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Поле со списком с полным перечнем сортов.
        /// </summary>
        private System.Windows.Forms.ComboBox flavorComboBox;

        /// <summary>
        /// список позиций достаточного запаса.
        /// </summary>
        private System.Windows.Forms.ListBox highList;

        /// <summary>
        /// список позиций недостаточного запаса.
        /// </summary>
        private System.Windows.Forms.ListBox lowList;

        /// <summary>
        ///  Данные представления на основе таблицы "Продажи", представляющие текущий день.
        /// </summary>
        private OperationsData.OperationsView view;

        private System.Windows.Forms.Label selectorLabel;

        private System.Windows.Forms.Label highLabel;

        private System.Windows.Forms.Label lowLabel;

        private System.Windows.Forms.GroupBox recommendationGroup;

        /// <summary>
        /// Отображается рекомендация, следует ли размещать незапланированный
        /// заказ. 
        /// </summary>
        private System.Windows.Forms.Label recommendationLabel;

        private System.Windows.Forms.Label orderLabel;

        /// <summary>
        /// Кнопка для создания незапланированного заказа.
        /// </summary>
        private System.Windows.Forms.Button createOrderButton;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnscheduledOrderControl));
            this.selectorLabel = new System.Windows.Forms.Label();
            this.flavorComboBox = new System.Windows.Forms.ComboBox();
            this.highLabel = new System.Windows.Forms.Label();
            this.lowLabel = new System.Windows.Forms.Label();
            this.highList = new System.Windows.Forms.ListBox();
            this.lowList = new System.Windows.Forms.ListBox();
            this.recommendationGroup = new System.Windows.Forms.GroupBox();
            this.createOrderButton = new System.Windows.Forms.Button();
            this.orderLabel = new System.Windows.Forms.Label();
            this.recommendationLabel = new System.Windows.Forms.Label();
            this.recommendationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectorLabel
            // 
            resources.ApplyResources(this.selectorLabel, "selectorLabel");
            this.selectorLabel.Name = "selectorLabel";
            // 
            // flavorComboBox
            // 
            this.flavorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flavorComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.flavorComboBox, "flavorComboBox");
            this.flavorComboBox.Name = "flavorComboBox";
            this.flavorComboBox.SelectedIndexChanged += new System.EventHandler(this.flavorComboBox_SelectedIndexChanged);
            // 
            // highLabel
            // 
            resources.ApplyResources(this.highLabel, "highLabel");
            this.highLabel.Name = "highLabel";
            // 
            // lowLabel
            // 
            resources.ApplyResources(this.lowLabel, "lowLabel");
            this.lowLabel.Name = "lowLabel";
            // 
            // highList
            // 
            this.highList.FormattingEnabled = true;
            resources.ApplyResources(this.highList, "highList");
            this.highList.Name = "highList";
            this.highList.Click += new System.EventHandler(this.inventoryList_Click);
            // 
            // lowList
            // 
            this.lowList.FormattingEnabled = true;
            resources.ApplyResources(this.lowList, "lowList");
            this.lowList.Name = "lowList";
            this.lowList.Click += new System.EventHandler(this.inventoryList_Click);
            // 
            // recommendationGroup
            // 
            this.recommendationGroup.Controls.Add(this.createOrderButton);
            this.recommendationGroup.Controls.Add(this.orderLabel);
            this.recommendationGroup.Controls.Add(this.recommendationLabel);
            resources.ApplyResources(this.recommendationGroup, "recommendationGroup");
            this.recommendationGroup.Name = "recommendationGroup";
            this.recommendationGroup.TabStop = false;
            // 
            // createOrderButton
            // 
            resources.ApplyResources(this.createOrderButton, "createOrderButton");
            this.createOrderButton.Name = "createOrderButton";
            this.createOrderButton.Click += new System.EventHandler(this.CreateOrderButton_Click);
            // 
            // orderLabel
            // 
            resources.ApplyResources(this.orderLabel, "orderLabel");
            this.orderLabel.Name = "orderLabel";
            // 
            // recommendationLabel
            // 
            resources.ApplyResources(this.recommendationLabel, "recommendationLabel");
            this.recommendationLabel.Name = "recommendationLabel";
            // 
            // UnscheduledOrderControl
            // 
            this.Controls.Add(this.recommendationGroup);
            this.Controls.Add(this.lowList);
            this.Controls.Add(this.highList);
            this.Controls.Add(this.lowLabel);
            this.Controls.Add(this.highLabel);
            this.Controls.Add(this.flavorComboBox);
            this.Controls.Add(this.selectorLabel);
            this.Name = "UnscheduledOrderControl";
            resources.ApplyResources(this, "$this");
            this.recommendationGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
