using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HorizTiltWheelDemo
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp); 

        public Form1()
        {
            InitializeComponent();

            panel1.MouseHWheel += new EventHandler<MouseEventArgs>(panel1_MouseHWheel);
        }

        void panel1_MouseHWheel(object sender, MouseEventArgs e)
        {
            label1.Text = string.Format("H Delta: {0}", e.Delta);
        }

        protected override void WndProc(ref Message m)
        {
            //send all mouse wheel messages to panel
            switch (m.Msg)
            {
                case Win32Messages.WM_MOUSEWHEEL:
                case Win32Messages.WM_MOUSEHWHEEL:
                    SendMessage(panel1.Handle, m.Msg, m.WParam, m.LParam);
                    m.Result = IntPtr.Zero;
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
