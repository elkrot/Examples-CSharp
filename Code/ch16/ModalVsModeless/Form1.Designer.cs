namespace ModalVsModeless
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
            this.buttonCreateModal = new System.Windows.Forms.Button();
            this.buttonCreateModeless = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCreateModal
            // 
            this.buttonCreateModal.Location = new System.Drawing.Point(12, 12);
            this.buttonCreateModal.Name = "buttonCreateModal";
            this.buttonCreateModal.Size = new System.Drawing.Size(98, 23);
            this.buttonCreateModal.TabIndex = 0;
            this.buttonCreateModal.Text = "Create Modal";
            this.buttonCreateModal.UseVisualStyleBackColor = true;
            this.buttonCreateModal.Click += new System.EventHandler(this.buttonCreateModal_Click);
            // 
            // buttonCreateModeless
            // 
            this.buttonCreateModeless.Location = new System.Drawing.Point(12, 53);
            this.buttonCreateModeless.Name = "buttonCreateModeless";
            this.buttonCreateModeless.Size = new System.Drawing.Size(98, 23);
            this.buttonCreateModeless.TabIndex = 1;
            this.buttonCreateModeless.Text = "Create Modeless";
            this.buttonCreateModeless.UseVisualStyleBackColor = true;
            this.buttonCreateModeless.Click += new System.EventHandler(this.buttonCreateModeless_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 235);
            this.Controls.Add(this.buttonCreateModeless);
            this.Controls.Add(this.buttonCreateModal);
            this.Name = "Form1";
            this.Text = "Modal vs. Modeless";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateModal;
        private System.Windows.Forms.Button buttonCreateModeless;
    }
}

