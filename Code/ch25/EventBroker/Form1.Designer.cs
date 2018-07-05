namespace EventBrokerDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myControl11 = new EventBrokerDemo.MyControl1();
            this.myControl21 = new EventBrokerDemo.MyControl2();
            this.myControl31 = new EventBrokerDemo.MyControl3();
            this.SuspendLayout();
            // 
            // myControl11
            // 
            this.myControl11.Location = new System.Drawing.Point(12, 12);
            this.myControl11.Name = "myControl11";
            this.myControl11.Size = new System.Drawing.Size(151, 96);
            this.myControl11.TabIndex = 0;
            // 
            // myControl21
            // 
            this.myControl21.Location = new System.Drawing.Point(169, 12);
            this.myControl21.Name = "myControl21";
            this.myControl21.Size = new System.Drawing.Size(150, 150);
            this.myControl21.TabIndex = 1;
            // 
            // myControl31
            // 
            this.myControl31.Location = new System.Drawing.Point(325, 12);
            this.myControl31.Name = "myControl31";
            this.myControl31.Size = new System.Drawing.Size(150, 150);
            this.myControl31.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 172);
            this.Controls.Add(this.myControl31);
            this.Controls.Add(this.myControl21);
            this.Controls.Add(this.myControl11);
            this.Name = "Form1";
            this.Text = "Event Broker Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private MyControl1 myControl11;
        private MyControl2 myControl21;
        private MyControl3 myControl31;
    }
}

