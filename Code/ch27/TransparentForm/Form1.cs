using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransparentForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackgroundImage = Properties.Resources.WindowTemplate;
            this.TransparencyKey = Color.White;

            this.Size = Properties.Resources.WindowTemplate.Size;

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //if we click anywhere on the form itself (not a child control),
            //then tell Windows we're clicking on the non-client area
            this.Capture = false;
            Win32.SendMessage(this.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HTCAPTION, 0);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
