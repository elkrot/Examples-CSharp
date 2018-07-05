using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LocWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //unless you have a languge pack installed, the UI Culture will not change with
            //the culture you set in Control Panel
            //this hack lets you manually control it
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            InitializeComponent();

            this.textBoxName.Text = "Joe";
            int number = 100001;
            //these formats are controlled by region settings in Control Panel
            this.textBoxNumber.Text = number.ToString("N0");
            this.textBoxBirthDate.Text = new DateTime(1979, 1, 15).ToLongDateString();

            this.labelMessage.Text = Properties.Resources.Message;
        }

    }
}
