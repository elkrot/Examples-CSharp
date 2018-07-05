using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SaveScreenLocation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RestoreLocation();
        }

        private void RestoreLocation()
        {
            Point location = Properties.Settings.Default.FormLocation;
            Size size = Properties.Settings.Default.FormSize;
            //make sure location is on a monitor
            bool isOnScreen = false;
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Contains(location))
                {
                    isOnScreen = true;
                }
            }
            //if our window isn't visible, put it on primary monitor
            if (!isOnScreen)
            {
                this.SetDesktopLocation(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Top);
            }

            //if too small, just reset to default
            if (size.Width < 10 || size.Height < 10)
            {
                Size = new Size(300, 300);
            }
        }

        private void SaveLocation()
        {
            Properties.Settings.Default.FormLocation = this.Location;
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Save();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            SaveLocation();
        }

        private void buttonMoveOff_Click(object sender, EventArgs e)
        {
            this.Location = new Point(10000, 10000);
            this.Close();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            UpdateLocation();
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            UpdateLocation();
        }

        private void UpdateLocation()
        {
            textBoxLocation.Text = string.Format("{0}, {1}", Location.X, Location.Y);
            textBoxSize.Text = string.Format("{0}, {1}", Size.Width, Size.Height);
        }

    }
}
