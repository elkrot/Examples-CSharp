using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.ApplicationServices;

namespace Windows7Features
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        private void buttonStdOFD_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            ofd.ShowDialog();
        }

        private void buttonWin7OFD_Click(object sender, EventArgs e)
        {
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog ofd = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            ofd.AddToMostRecentlyUsedList = true;
            ofd.IsFolderPicker = true;
            //allows you to pick things like Control Panel and libraries
            ofd.AllowNonFileSystemItems = true;

            if (ofd.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.OK)
            {
                ShellObject shellObj = ofd.FileAsShellObject;
                ShellLibrary library = shellObj as ShellLibrary;
                if (library != null)
                {
                    textBoxInfo.AppendText("You picked a library: " + library.Name + ", Type: " + library.LibraryType.ToString());
                    foreach (ShellFileSystemFolder folder in (ShellLibrary)shellObj)
                    {
                        textBoxInfo.AppendText("\t" + folder.Path);                        
                    }
                }
                textBoxInfo.AppendText(Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetPowerInfo();
        }


        private void GetPowerInfo()
        {
            this.labelPowerPersonality.Text = PowerManager.PowerPersonality.ToString();
            this.checkBoxBatteryPresent.Checked = PowerManager.IsBatteryPresent;
            this.checkBoxMonitorOn.Checked = PowerManager.IsMonitorOn;
            this.checkBoxUpsPresent.Checked = PowerManager.IsUpsPresent;
            this.labelPowerSource.Text = PowerManager.PowerSource.ToString();
        }


        

        
    }
}
