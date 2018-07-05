using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StripHtml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxStripped.Text = StripHtml(textBoxHtml.Text);
        }

        private string StripHtml(string source)
        {
            string[] patterns = {
                @"<(.|\n)*?>",          //general HTML tags
                @"<script.*?</script>"  //script tags
                                };
            string stripped = source;
            foreach (string pattern in patterns)
            {
                stripped = Regex.Replace(stripped, pattern, string.Empty);
            }
                       
            return stripped;
        }
    }
}
