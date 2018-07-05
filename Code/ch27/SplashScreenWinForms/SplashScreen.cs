using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SplashScreenWinForms
{
    public partial class SplashScreen : Form
    {
        public SplashScreen(Image image)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackgroundImage = image;
            this.Size = image.Size;

            this.labelStatus.BackColor = Color.Transparent;
        }

        public void UpdateStatus(string status, int percent)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    UpdateStatus(status, percent);                    
                }));
            }
            else
            {
                progressBar.Value = percent;
                labelStatus.Text = status;
            }
        }
    }
}
