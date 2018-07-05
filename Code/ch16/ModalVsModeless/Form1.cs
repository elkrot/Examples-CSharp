using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModalVsModeless
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCreateModal_Click(object sender, EventArgs e)
        {
            //show form modally
            PopupForm form = new PopupForm();
            form.ShowDialog(this);
        }

        private void buttonCreateModeless_Click(object sender, EventArgs e)
        {
            //show form modelessly
            PopupForm form = new PopupForm();
            //setting the parent will allow better behavior of child and parent
            //windows, especially when dealing with active windows and minimization
            form.Show(this);
        }

        
    }
}
