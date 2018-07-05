using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DerivedForms
{
    public partial class InheritedForm : DerivedForms.BaseForm
    {
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private CheckBox checkBox1;

        private System.ComponentModel.IContainer components = null;

        public InheritedForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // listView1
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(15, 57);
            this.listView1.View = System.Windows.Forms.View.Details;
            // columnHeader1
            this.columnHeader1.Text = "Items";
            // columnHeader2
            this.columnHeader2.Text = "Attributes";
            // checkBox1
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 126);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Added checkbox";
            this.checkBox1.UseVisualStyleBackColor = true;
            // InheritedForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(243, 149);
            this.Controls.Add(this.checkBox1);
            this.Name = "InheritedForm";
            this.Text = "InheritedForm";
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}