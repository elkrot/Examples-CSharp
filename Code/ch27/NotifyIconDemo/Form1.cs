using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotifyIconDemo
{
    public partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;

        private NotifyIcon notifyIcon;
        private ToolStripMenuItem sayHelloToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ContextMenuStrip menu;

        public Form1()
        {
            InitializeComponent();
        }

        private void sayHelloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDefaultAction();
        }

        private void DoDefaultAction()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            MessageBox.Show("Hello");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoDefaultAction();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.sayHelloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sayHelloToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(153, 76);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.menu;
            this.notifyIcon.Icon = global::NotifyIconDemo.Properties.Resources.DemoIcon;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // sayHelloToolStripMenuItem
            // 
            this.sayHelloToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.sayHelloToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sayHelloToolStripMenuItem.Image")));
            this.sayHelloToolStripMenuItem.Name = "sayHelloToolStripMenuItem";
            this.sayHelloToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sayHelloToolStripMenuItem.Text = "&Say Hello";
            this.sayHelloToolStripMenuItem.Click += new System.EventHandler(this.sayHelloToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 191);
            this.Name = "Form1";
            this.Text = "Notify Icon Demo";
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
