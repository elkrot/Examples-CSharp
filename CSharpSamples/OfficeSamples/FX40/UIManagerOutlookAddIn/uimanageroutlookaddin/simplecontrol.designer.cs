// ЭТОТ КОД И СВЕДЕНИЯ ПРЕДОСТАВЛЯЮТСЯ "КАК ЕСТЬ" БЕЗ КАКИХ-ЛИБО ГАРАНТИЙ,
// ЯВНЫХ ИЛИ ПОДРАЗУМЕВАЕМЫХ, ВКЛЮЧАЯ, НО НЕ ОГРАНИЧИВАЯСЬ ПЕРЕЧИСЛЕННЫМ,
// ПОДРАЗУМЕВАЕМЫЕ ГАРАНТИИ ПРИГОДНОСТИ ДЛЯ ПРОДАЖИ И (ИЛИ) ПРИМЕНИМОСТИ ДЛЯ
// ОПРЕДЕЛЕННОЙ ЦЕЛИ.
//
// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace UiManagerOutlookAddIn
{
    partial class SimpleControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleControl));
            this._coffeePicture = new System.Windows.Forms.PictureBox();
            this._coffeeList = new System.Windows.Forms.ListBox();
            this._coffeeGroup = new System.Windows.Forms.GroupBox();
            this._closeCoffeeList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._coffeePicture)).BeginInit();
            this._coffeeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // _coffeePicture
            // 
            this._coffeePicture.Image = global::UiManagerOutlookAddIn.Properties.Resources.espressoCup_tall;
            resources.ApplyResources(this._coffeePicture, "_coffeePicture");
            this._coffeePicture.Name = "_coffeePicture";
            this._coffeePicture.TabStop = false;
            // 
            // _coffeeList
            // 
            resources.ApplyResources(this._coffeeList, "_coffeeList");
            this._coffeeList.FormattingEnabled = true;
            this._coffeeList.Name = "_coffeeList";
            // 
            // _coffeeGroup
            // 
            this._coffeeGroup.Controls.Add(this._closeCoffeeList);
            this._coffeeGroup.Controls.Add(this._coffeeList);
            this._coffeeGroup.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this._coffeeGroup, "_coffeeGroup");
            this._coffeeGroup.Name = "_coffeeGroup";
            this._coffeeGroup.TabStop = false;
            // 
            // _closeCoffeeList
            // 
            this._closeCoffeeList.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this._closeCoffeeList, "_closeCoffeeList");
            this._closeCoffeeList.Name = "_closeCoffeeList";
            this._closeCoffeeList.UseVisualStyleBackColor = true;
            this._closeCoffeeList.Click += new System.EventHandler(this.closeCoffeeList_Click);
            // 
            // SimpleControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.Controls.Add(this._coffeeGroup);
            this.Controls.Add(this._coffeePicture);
            this.Name = "SimpleControl";
            ((System.ComponentModel.ISupportInitialize)(this._coffeePicture)).EndInit();
            this._coffeeGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _coffeePicture;
        internal System.Windows.Forms.ListBox _coffeeList;
        internal System.Windows.Forms.GroupBox _coffeeGroup;
        internal System.Windows.Forms.Button _closeCoffeeList;

    }
}
