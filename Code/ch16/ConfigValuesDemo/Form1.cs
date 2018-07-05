using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigValuesDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.textBoxAppConfigValue.Text = Properties.Settings.Default.MyAppSetting;
            this.textBoxUserConfigValue.Text = Properties.Settings.Default.MyUserSetting;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Properties.Settings.Default.MyUserSetting = textBoxUserConfigValue.Text;
            //can't do this--app settings are read-only!
            //Properties.Settings.Default.MyAppSetting = textBoxAppConfigValue.Text;

            //better save if you want to see the settings next time the app runs
            Properties.Settings.Default.Save();
        }
    }
}
