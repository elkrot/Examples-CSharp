namespace WCFClient
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
            if (disposing)
            {
                if (fsClient != null)
                {
                    fsClient.Close();
                }
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxGetSubDirs = new System.Windows.Forms.TextBox();
            this.buttonGetSubDirs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGetFiles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRetrieveFile = new System.Windows.Forms.TextBox();
            this.numericUpDownBytesToRead = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGetFiles = new System.Windows.Forms.Button();
            this.buttonGetFileContents = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBytesToRead)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Get subdirectories:";
            // 
            // textBoxGetSubDirs
            // 
            this.textBoxGetSubDirs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGetSubDirs.Location = new System.Drawing.Point(116, 9);
            this.textBoxGetSubDirs.Name = "textBoxGetSubDirs";
            this.textBoxGetSubDirs.Size = new System.Drawing.Size(479, 20);
            this.textBoxGetSubDirs.TabIndex = 1;
            this.textBoxGetSubDirs.Text = "C:\\";
            // 
            // buttonGetSubDirs
            // 
            this.buttonGetSubDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetSubDirs.Location = new System.Drawing.Point(601, 8);
            this.buttonGetSubDirs.Name = "buttonGetSubDirs";
            this.buttonGetSubDirs.Size = new System.Drawing.Size(47, 23);
            this.buttonGetSubDirs.TabIndex = 2;
            this.buttonGetSubDirs.Text = "Go";
            this.buttonGetSubDirs.UseVisualStyleBackColor = true;
            this.buttonGetSubDirs.Click += new System.EventHandler(this.buttonGetSubDirs_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Get files:";
            // 
            // textBoxGetFiles
            // 
            this.textBoxGetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGetFiles.Location = new System.Drawing.Point(116, 38);
            this.textBoxGetFiles.Name = "textBoxGetFiles";
            this.textBoxGetFiles.Size = new System.Drawing.Size(479, 20);
            this.textBoxGetFiles.TabIndex = 4;
            this.textBoxGetFiles.Text = "C:\\Windows\\System32\\drivers\\etc\\";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Get file contents:";
            // 
            // textBoxRetrieveFile
            // 
            this.textBoxRetrieveFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRetrieveFile.Location = new System.Drawing.Point(116, 67);
            this.textBoxRetrieveFile.Name = "textBoxRetrieveFile";
            this.textBoxRetrieveFile.Size = new System.Drawing.Size(382, 20);
            this.textBoxRetrieveFile.TabIndex = 6;
            this.textBoxRetrieveFile.Text = "C:\\Windows\\System32\\drivers\\etc\\hosts";
            // 
            // numericUpDownBytesToRead
            // 
            this.numericUpDownBytesToRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownBytesToRead.Location = new System.Drawing.Point(542, 67);
            this.numericUpDownBytesToRead.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.numericUpDownBytesToRead.Name = "numericUpDownBytesToRead";
            this.numericUpDownBytesToRead.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownBytesToRead.TabIndex = 7;
            this.numericUpDownBytesToRead.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "bytes:";
            // 
            // buttonGetFiles
            // 
            this.buttonGetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetFiles.Location = new System.Drawing.Point(601, 37);
            this.buttonGetFiles.Name = "buttonGetFiles";
            this.buttonGetFiles.Size = new System.Drawing.Size(47, 23);
            this.buttonGetFiles.TabIndex = 9;
            this.buttonGetFiles.Text = "Go";
            this.buttonGetFiles.UseVisualStyleBackColor = true;
            this.buttonGetFiles.Click += new System.EventHandler(this.buttonGetFiles_Click);
            // 
            // buttonGetFileContents
            // 
            this.buttonGetFileContents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetFileContents.Location = new System.Drawing.Point(601, 66);
            this.buttonGetFileContents.Name = "buttonGetFileContents";
            this.buttonGetFileContents.Size = new System.Drawing.Size(47, 23);
            this.buttonGetFileContents.TabIndex = 10;
            this.buttonGetFileContents.Text = "Go";
            this.buttonGetFileContents.UseVisualStyleBackColor = true;
            this.buttonGetFileContents.Click += new System.EventHandler(this.buttonGetFileContents_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Location = new System.Drawing.Point(3, 16);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutput.Size = new System.Drawing.Size(630, 102);
            this.textBoxOutput.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxOutput);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(636, 121);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 226);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonGetFileContents);
            this.Controls.Add(this.buttonGetFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownBytesToRead);
            this.Controls.Add(this.textBoxRetrieveFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxGetFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGetSubDirs);
            this.Controls.Add(this.textBoxGetSubDirs);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "WCF Client";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBytesToRead)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxGetSubDirs;
        private System.Windows.Forms.Button buttonGetSubDirs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGetFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRetrieveFile;
        private System.Windows.Forms.NumericUpDown numericUpDownBytesToRead;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGetFiles;
        private System.Windows.Forms.Button buttonGetFileContents;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

