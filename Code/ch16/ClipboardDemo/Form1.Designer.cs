namespace ClipboardDemo
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderSex = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAge = new System.Windows.Forms.ColumnHeader();
            this.buttonAddRandomPerson = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonCut = new System.Windows.Forms.Button();
            this.buttonPasteToList = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderSex,
            this.columnHeaderAge});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(451, 97);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSex
            // 
            this.columnHeaderSex.DisplayIndex = 0;
            this.columnHeaderSex.Text = "Sex";
            this.columnHeaderSex.Width = 98;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.DisplayIndex = 1;
            this.columnHeaderName.Text = "Name";
            // 
            // columnHeaderAge
            // 
            this.columnHeaderAge.Text = "Age";
            // 
            // buttonAddRandomPerson
            // 
            this.buttonAddRandomPerson.Location = new System.Drawing.Point(13, 113);
            this.buttonAddRandomPerson.Name = "buttonAddRandomPerson";
            this.buttonAddRandomPerson.Size = new System.Drawing.Size(122, 23);
            this.buttonAddRandomPerson.TabIndex = 1;
            this.buttonAddRandomPerson.Text = "Add Random Person";
            this.buttonAddRandomPerson.UseVisualStyleBackColor = true;
            this.buttonAddRandomPerson.Click += new System.EventHandler(this.buttonAddRandomPerson_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(272, 113);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(53, 23);
            this.buttonCopy.TabIndex = 2;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCut
            // 
            this.buttonCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCut.Location = new System.Drawing.Point(331, 113);
            this.buttonCut.Name = "buttonCut";
            this.buttonCut.Size = new System.Drawing.Size(53, 23);
            this.buttonCut.TabIndex = 3;
            this.buttonCut.Text = "Cut";
            this.buttonCut.UseVisualStyleBackColor = true;
            this.buttonCut.Click += new System.EventHandler(this.buttonCut_Click);
            // 
            // buttonPasteToList
            // 
            this.buttonPasteToList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPasteToList.Location = new System.Drawing.Point(390, 113);
            this.buttonPasteToList.Name = "buttonPasteToList";
            this.buttonPasteToList.Size = new System.Drawing.Size(73, 23);
            this.buttonPasteToList.TabIndex = 4;
            this.buttonPasteToList.Text = "Paste to list";
            this.buttonPasteToList.UseVisualStyleBackColor = true;
            this.buttonPasteToList.Click += new System.EventHandler(this.buttonPasteToList_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(13, 160);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(387, 65);
            this.textBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Scratch Pad:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(142, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 39);
            this.label2.TabIndex = 7;
            this.label2.Text = "Try pasting into the textbox below and a graphics program as well";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 237);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonPasteToList);
            this.Controls.Add(this.buttonCut);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonAddRandomPerson);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Clipboard Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderSex;
        private System.Windows.Forms.ColumnHeader columnHeaderAge;
        private System.Windows.Forms.Button buttonAddRandomPerson;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonCut;
        private System.Windows.Forms.Button buttonPasteToList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

