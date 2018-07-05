namespace FlickerFree
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxFlickerFree = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.drawPanel = new FlickerFree.DrawPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBoxFlickerFree);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(146, 489);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxFlickerFree
            // 
            this.checkBoxFlickerFree.AutoSize = true;
            this.checkBoxFlickerFree.Location = new System.Drawing.Point(12, 12);
            this.checkBoxFlickerFree.Name = "checkBoxFlickerFree";
            this.checkBoxFlickerFree.Size = new System.Drawing.Size(78, 17);
            this.checkBoxFlickerFree.TabIndex = 0;
            this.checkBoxFlickerFree.Text = "Flicker-free";
            this.checkBoxFlickerFree.UseVisualStyleBackColor = true;
            this.checkBoxFlickerFree.CheckedChanged += new System.EventHandler(this.checkBoxFlickerFree_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Just start clicking!";
            // 
            // drawPanel
            // 
            this.drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPanel.DoubleBuffered = false;
            this.drawPanel.Location = new System.Drawing.Point(146, 0);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(515, 489);
            this.drawPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 53);
            this.label2.TabIndex = 2;
            this.label2.Text = "Then try dragging the boxes";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 489);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "FlickerFree";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxFlickerFree;
        private DrawPanel drawPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

