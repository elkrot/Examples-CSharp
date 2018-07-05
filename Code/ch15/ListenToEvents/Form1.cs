using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListenToEvents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //add our own method to the button's Click delegate list
            button1.Click += new EventHandler(button1_Click);
        }

        void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is too easy");
        }
    }
}
