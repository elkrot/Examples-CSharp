using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DefineEvent
{
    public partial class Form1 : Form
    {
        ProgramData _data;

        public Form1()
        {
            InitializeComponent();

            _data = new ProgramData();
            _data.LoadStarted += new EventHandler<EventArgs>(_data_LoadStarted);
            _data.LoadEnded += new EventHandler<ProgramDataEventArgs>(_data_LoadEnded);

            _data.BeginLoad();
        }

        void _data_LoadStarted(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                //if we're not on the UI thread, call recursively, but on the UI thread
                Invoke(new EventHandler<EventArgs>(_data_LoadStarted), sender, e);
            }
            else
            {
                textBoxLog.AppendText("Load started" + Environment.NewLine);
            }
        }

        void _data_LoadEnded(object sender, ProgramDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new EventHandler<ProgramDataEventArgs>(_data_LoadEnded), sender, e);
            }
            else
            {
                textBoxLog.AppendText(string.Format("Load ended (elapsed: {0})" + Environment.NewLine, e.LoadTime));
            }
        }
    }
}
