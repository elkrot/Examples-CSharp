using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ToolAndStatusAndSplit
{
    public partial class Form1 : Form
    {
        private ToolStripContainer toolStripContainer;
        private StatusStrip statusStrip;
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripButtonExit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripComboBox toolStripComboBoxDrives;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripProgressBar toolStripProgressBar;
        private SplitContainer splitContainer;
        
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TextBox textView;
        private TreeView treeView;
        public Form1()
        {
            InitializeComponent();
        }

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
        
        #region initialization

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.textView = new System.Windows.Forms.TextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxDrives = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(840, 369);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(840, 440);
            this.toolStripContainer.TabIndex = 0;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripProgressBar });
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(840, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.textView);
            this.splitContainer.Size = new System.Drawing.Size(840, 369);
            this.splitContainer.SplitterDistance = 280;
            this.splitContainer.TabIndex = 2;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(280, 369);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // textView
            // 
            this.textView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textView.Location = new System.Drawing.Point(0, 0);
            this.textView.Multiline = true;
            this.textView.Name = "textView";
            this.textView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textView.Size = new System.Drawing.Size(556, 369);
            this.textView.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonExit, toolStripComboBoxDrives});
            this.toolStrip.Location = new System.Drawing.Point(3, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(43, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(840, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.DropDownItems.Add(copyToolStripMenuItem);
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExit.Image")));
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExit.Text = "Exit";
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBoxDrives
            // 
            this.toolStripComboBoxDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxDrives.Name = "toolStripComboBoxDrives";
            this.toolStripComboBoxDrives.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxDrives.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxDrives_SelectedIndexChanged);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(840, 440);
            this.Controls.Add(this.toolStripContainer);
            this.Name = "Form1";
            this.Text = "Menu, tool, status, and split demo";
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        
#endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FillDriveCombo();
        }

        

        #region Combo-box related
        private void FillDriveCombo()
        {
            this.toolStripComboBoxDrives.Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo info in drives)
            {
                string name = string.Format("{0} - {1}", info.Name, info.IsReady ? info.VolumeLabel : "<unknown>");
                this.toolStripComboBoxDrives.Items.Add(name);
            }
        }

        private void SelectSystemDrive()
        {
            string sysDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            int index = toolStripComboBoxDrives.FindString(sysDrive);
            if (index >= 0)
            {
                toolStripComboBoxDrives.SelectedIndex = index;
            }
        }

        void toolStripComboBoxDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(FillFolders));
            thread.Start(GetSelectedDrive());
            thread.IsBackground = true;
            toolStripComboBoxDrives.Enabled = false;
        }

        private string GetSelectedDrive()
        {
            string[] parts = (toolStripComboBoxDrives.SelectedItem as string).Split(' ', '-');
            if (parts.Length > 0)
            {
                return parts[0];
            }
            return null;
        }
        #endregion

        #region Tree-related

        private void FillFolders(object param)
        {
            string drive = param as string;
            if (string.IsNullOrEmpty(drive))
                return;
            
            TreeNode root = new TreeNode(drive);
            if (treeView.InvokeRequired)
            {
                treeView.Invoke(new MethodInvoker(delegate {
                    treeView.Nodes.Clear();
                    toolStripProgressBar.Style = ProgressBarStyle.Marquee;
                    this.toolStripStatusLabel.Text = "Reading folders...";
                    treeView.Nodes.Add(root); 
                }));
            }
            
            FillFolders(root, drive, 1);
            if (treeView.InvokeRequired)
            {
                treeView.Invoke(new MethodInvoker(delegate
                    {
                        root.Expand();
                        toolStripComboBoxDrives.Enabled = true;
                        toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                        toolStripStatusLabel.Text = "Ready";
                    }));
            }
        }

        const int MaxDepth = 5;

        private void FillFolders(TreeNode parent, string path, int depth)
        {
            if (depth > MaxDepth)
                return;
            try
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string dir in directories)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(dir));
                    node.Tag = dir;
                    if (treeView.InvokeRequired)
                    {
                        treeView.Invoke(new MethodInvoker(
                            delegate
                            {
                                parent.Nodes.Add(node);
                            }));
                    }

                    FillFolders(node, dir, depth + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
            catch (IOException)
            {
                return;
            }
        }

        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string fullPath = e.Node.Tag as string;
            
            if (!string.IsNullOrEmpty(fullPath))
            {
                textView.Clear();
                try
                {
                    string[] files = Directory.GetFiles(fullPath);
                    foreach (string file in files)
                    {
                        FileInfo info = new FileInfo(file);
                        string line = string.Format("{0}\t\t{1:N0}KB\t\t{2}", Path.GetFileName(file), info.Length / 1024, info.LastWriteTime);
                        textView.AppendText(line + Environment.NewLine);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    textView.Text = ex.Message;
                }
            }
        }
        #endregion
        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.copyToolStripMenuItem.Enabled = (textView.SelectionLength > 0);
        }

        void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textView.SelectionLength > 0)
            {
                textView.Copy();
            }
        }

        void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        
    }
}
