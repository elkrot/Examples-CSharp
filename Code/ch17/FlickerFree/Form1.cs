using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FlickerFree
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }


        private void checkBoxFlickerFree_CheckedChanged(object sender, EventArgs e)
        {
            drawPanel.DoubleBuffered = !drawPanel.DoubleBuffered;
        }
    }
}
