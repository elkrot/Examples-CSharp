namespace DerivedForms
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
            this.buttonShowBaseForm = new System.Windows.Forms.Button();
            this.buttonShowInheritedForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonShowBaseForm
            // 
            this.buttonShowBaseForm.Location = new System.Drawing.Point(13, 12);
            this.buttonShowBaseForm.Name = "buttonShowBaseForm";
            this.buttonShowBaseForm.Size = new System.Drawing.Size(113, 23);
            this.buttonShowBaseForm.TabIndex = 0;
            this.buttonShowBaseForm.Text = "Show BaseForm";
            this.buttonShowBaseForm.UseVisualStyleBackColor = true;
            this.buttonShowBaseForm.Click += new System.EventHandler(this.buttonShowBaseForm_Click);
            // 
            // buttonShowInheritedForm
            // 
            this.buttonShowInheritedForm.Location = new System.Drawing.Point(13, 41);
            this.buttonShowInheritedForm.Name = "buttonShowInheritedForm";
            this.buttonShowInheritedForm.Size = new System.Drawing.Size(113, 23);
            this.buttonShowInheritedForm.TabIndex = 1;
            this.buttonShowInheritedForm.Text = "Show InheritedForm";
            this.buttonShowInheritedForm.UseVisualStyleBackColor = true;
            this.buttonShowInheritedForm.Click += new System.EventHandler(this.buttonShowInheritedForm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 124);
            this.Controls.Add(this.buttonShowInheritedForm);
            this.Controls.Add(this.buttonShowBaseForm);
            this.Name = "Form1";
            this.Text = "Demo Form Derivation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShowBaseForm;
        private System.Windows.Forms.Button buttonShowInheritedForm;
    }
}

