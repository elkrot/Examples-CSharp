using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DerivedForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonShowBaseForm_Click(object sender, EventArgs e)
        {
            BaseForm form = new BaseForm();
            form.Show();
        }

        private void buttonShowInheritedForm_Click(object sender, EventArgs e)
        {
            InheritedForm form = new InheritedForm();
            form.Show();
        }
    }
}
