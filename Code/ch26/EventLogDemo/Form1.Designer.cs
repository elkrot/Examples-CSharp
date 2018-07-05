namespace EventLogDemo
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
            this.buttonLogIt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonError = new System.Windows.Forms.RadioButton();
            this.radioButtonWarn = new System.Windows.Forms.RadioButton();
            this.radioButtonInfo = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonViewEvents = new System.Windows.Forms.Button();
            this.buttonCreateSource = new EventLogDemo.UACButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogIt
            // 
            this.buttonLogIt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogIt.Location = new System.Drawing.Point(419, 134);
            this.buttonLogIt.Name = "buttonLogIt";
            this.buttonLogIt.Size = new System.Drawing.Size(75, 23);
            this.buttonLogIt.TabIndex = 0;
            this.buttonLogIt.Text = "Log it!";
            this.buttonLogIt.UseVisualStyleBackColor = true;
            this.buttonLogIt.Click += new System.EventHandler(this.buttonLogIt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message:";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Location = new System.Drawing.Point(71, 55);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(423, 73);
            this.textBoxMessage.TabIndex = 2;
            this.textBoxMessage.Text = "Hello, World!";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonError);
            this.groupBox1.Controls.Add(this.radioButtonWarn);
            this.groupBox1.Controls.Add(this.radioButtonInfo);
            this.groupBox1.Location = new System.Drawing.Point(266, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 42);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log Entry Type";
            // 
            // radioButtonError
            // 
            this.radioButtonError.AutoSize = true;
            this.radioButtonError.Location = new System.Drawing.Point(163, 19);
            this.radioButtonError.Name = "radioButtonError";
            this.radioButtonError.Size = new System.Drawing.Size(47, 17);
            this.radioButtonError.TabIndex = 2;
            this.radioButtonError.Text = "Error";
            this.radioButtonError.UseVisualStyleBackColor = true;
            this.radioButtonError.CheckedChanged += new System.EventHandler(this.OnEntryTypeChanged);
            // 
            // radioButtonWarn
            // 
            this.radioButtonWarn.AutoSize = true;
            this.radioButtonWarn.Location = new System.Drawing.Point(91, 19);
            this.radioButtonWarn.Name = "radioButtonWarn";
            this.radioButtonWarn.Size = new System.Drawing.Size(65, 17);
            this.radioButtonWarn.TabIndex = 1;
            this.radioButtonWarn.Text = "Warning";
            this.radioButtonWarn.UseVisualStyleBackColor = true;
            this.radioButtonWarn.CheckedChanged += new System.EventHandler(this.OnEntryTypeChanged);
            // 
            // radioButtonInfo
            // 
            this.radioButtonInfo.AutoSize = true;
            this.radioButtonInfo.Checked = true;
            this.radioButtonInfo.Location = new System.Drawing.Point(7, 19);
            this.radioButtonInfo.Name = "radioButtonInfo";
            this.radioButtonInfo.Size = new System.Drawing.Size(77, 17);
            this.radioButtonInfo.TabIndex = 0;
            this.radioButtonInfo.TabStop = true;
            this.radioButtonInfo.Text = "Information";
            this.radioButtonInfo.UseVisualStyleBackColor = true;
            this.radioButtonInfo.CheckedChanged += new System.EventHandler(this.OnEntryTypeChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(124, 25);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(81, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Event ID:";
            // 
            // buttonViewEvents
            // 
            this.buttonViewEvents.Location = new System.Drawing.Point(235, 134);
            this.buttonViewEvents.Name = "buttonViewEvents";
            this.buttonViewEvents.Size = new System.Drawing.Size(75, 23);
            this.buttonViewEvents.TabIndex = 7;
            this.buttonViewEvents.Text = "View Events";
            this.buttonViewEvents.UseVisualStyleBackColor = true;
            this.buttonViewEvents.Click += new System.EventHandler(this.buttonViewEvents_Click);
            // 
            // buttonCreateSource
            // 
            this.buttonCreateSource.Location = new System.Drawing.Point(12, 134);
            this.buttonCreateSource.Name = "buttonCreateSource";
            this.buttonCreateSource.ShowShield = true;
            this.buttonCreateSource.Size = new System.Drawing.Size(119, 23);
            this.buttonCreateSource.TabIndex = 3;
            this.buttonCreateSource.Text = "Create Source";
            this.buttonCreateSource.UseVisualStyleBackColor = true;
            this.buttonCreateSource.Click += new System.EventHandler(this.buttonCreateSource_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 169);
            this.Controls.Add(this.buttonViewEvents);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCreateSource);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLogIt);
            this.Name = "Form1";
            this.Text = "EventLog Demo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogIt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMessage;
        private UACButton buttonCreateSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonError;
        private System.Windows.Forms.RadioButton radioButtonWarn;
        private System.Windows.Forms.RadioButton radioButtonInfo;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonViewEvents;
    }
}

