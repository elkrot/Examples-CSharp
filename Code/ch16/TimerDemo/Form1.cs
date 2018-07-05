using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimerDemo
{
    public partial class Form1 : Form
    {
        private Timer _timer;
        bool _tick = true;
        bool _stopped = true;

        public Form1()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = 1000;//1 second
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            labelOutput.Text = _tick ? "Tick" : "Tock";
            _tick = !_tick;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (_stopped)
            {
                _timer.Enabled = true;
                buttonStart.Text = "&Stop";
            }
            else
            {
                _timer.Enabled = false;
                buttonStart.Text = "&Start";
            }
            _stopped = !_stopped;
        }
    }
}
