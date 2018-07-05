using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            if (radioButtonUrl.Checked)
            {
                this.webBrowser1.Navigate(textBoxUrl.Text);
            }
            else
            {
                this.webBrowser1.DocumentText = textBoxHTML.Text;
            }
        }

        private void OnRadioCheckedChanged(object sender, EventArgs e)
        {
            textBoxUrl.Enabled = radioButtonUrl.Checked;
            textBoxHTML.Enabled = radioButtonHTML.Checked;
        }
    }
}
