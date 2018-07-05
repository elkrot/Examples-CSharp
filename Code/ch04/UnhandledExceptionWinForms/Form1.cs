using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UnhandledExceptionWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        }
        
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Trapped unhandled exception");
            sb.AppendLine(e.Exception.ToString());

            MessageBox.Show(sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new InvalidOperationException("Oops");
        }
    }
}
