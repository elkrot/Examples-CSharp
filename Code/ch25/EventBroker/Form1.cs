using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventBrokerDemo
{
    public partial class Form1 : Form
    {
        //a single event broker to tie all controls together
        EventBroker _broker = new EventBroker();

        public Form1()
        {
            InitializeComponent();

            myControl11.SetEventBroker(_broker);
            myControl21.SetEventBroker(_broker);
            myControl31.SetEventBroker(_broker);
        }
    }
}
