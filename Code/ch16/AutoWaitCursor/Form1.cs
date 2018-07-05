using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoWaitCursor
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
            MessageBox.Show("Unhandled exception (which I'm handling)");
        }

        private void buttonAutoWait_Click(object sender, EventArgs e)
        {
            using (new AutoWaitCursor(this))
            {
                throw new Exception("Ooops, something happened!");
            }
        }

        private void buttonOldWait_Click(object sender, EventArgs e)
        {
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            throw new Exception("Ooops, something happened!");
            this.Cursor = oldCursor;
        }

        private void buttonResetCursor_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
    }
}
