using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WpfInWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //create our WPF Control and add it to a hosting control

            MyWpfControl wpfControl = new MyWpfControl();
            wpfControl.ButtonClicked += new EventHandler<EventArgs>(OnButtonClicked);
            ElementHost host = new ElementHost();
            host.Left = 5;
            host.Top = 100;
            host.Width = 160;
            host.Height = 66;
            host.Child = wpfControl;

            this.Controls.Add(host);
        }

        private void OnButtonClicked(object source, EventArgs e)
        {
            MessageBox.Show("WPF Button clicked");
        }
    }
}
