namespace BatchEvents
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
            this.listViewOutput = new System.Windows.Forms.ListView();
            this.buttonOneAtATime = new System.Windows.Forms.Button();
            this.buttonUpdateBatch = new System.Windows.Forms.Button();
            this.labelElapsed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewOutput
            // 
            this.listViewOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewOutput.Location = new System.Drawing.Point(126, 12);
            this.listViewOutput.Name = "listViewOutput";
            this.listViewOutput.Size = new System.Drawing.Size(175, 343);
            this.listViewOutput.TabIndex = 0;
            this.listViewOutput.UseCompatibleStateImageBehavior = false;
            this.listViewOutput.View = System.Windows.Forms.View.List;
            // 
            // buttonOneAtATime
            // 
            this.buttonOneAtATime.Location = new System.Drawing.Point(13, 13);
            this.buttonOneAtATime.Name = "buttonOneAtATime";
            this.buttonOneAtATime.Size = new System.Drawing.Size(96, 23);
            this.buttonOneAtATime.TabIndex = 1;
            this.buttonOneAtATime.Text = "Upate 1-at-a-time";
            this.buttonOneAtATime.UseVisualStyleBackColor = true;
            this.buttonOneAtATime.Click += new System.EventHandler(this.buttonOneAtATime_Click);
            // 
            // buttonUpdateBatch
            // 
            this.buttonUpdateBatch.Location = new System.Drawing.Point(13, 42);
            this.buttonUpdateBatch.Name = "buttonUpdateBatch";
            this.buttonUpdateBatch.Size = new System.Drawing.Size(96, 23);
            this.buttonUpdateBatch.TabIndex = 2;
            this.buttonUpdateBatch.Text = "Update as batch";
            this.buttonUpdateBatch.UseVisualStyleBackColor = true;
            this.buttonUpdateBatch.Click += new System.EventHandler(this.buttonUpdateBatch_Click);
            // 
            // labelElapsed
            // 
            this.labelElapsed.AutoSize = true;
            this.labelElapsed.Location = new System.Drawing.Point(12, 85);
            this.labelElapsed.Name = "labelElapsed";
            this.labelElapsed.Size = new System.Drawing.Size(49, 13);
            this.labelElapsed.TabIndex = 3;
            this.labelElapsed.Text = "00:00:00";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 367);
            this.Controls.Add(this.labelElapsed);
            this.Controls.Add(this.buttonUpdateBatch);
            this.Controls.Add(this.buttonOneAtATime);
            this.Controls.Add(this.listViewOutput);
            this.Name = "Form1";
            this.Text = "Batch Events";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewOutput;
        private System.Windows.Forms.Button buttonOneAtATime;
        private System.Windows.Forms.Button buttonUpdateBatch;
        private System.Windows.Forms.Label labelElapsed;
    }
}

