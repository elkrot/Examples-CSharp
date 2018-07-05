using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnValuesChanged(object sender, EventArgs e)
        {
            int r = redControl.Value;
            int g = greenControl.Value;
            int b = blueControl.Value;

            colorControl.BackColor = Color.FromArgb(r, g, b);
        }
    }
}
