using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EventDemo
{
    public partial class Form1 : Form
    {
        ManualResetEvent _manualEvent = new ManualResetEvent(false);
        AutoResetEvent _autoEvent = new AutoResetEvent(false);

        private ProgressBar[] _progressBars = new ProgressBar[3];
        private Thread[] _threads = new Thread[3];

        const int MaxValue = 1000000;

        bool _manual = true;

        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            _manualEvent.Reset();
            _autoEvent.Reset();

            _progressBars[0] = progressBar1;
            _progressBars[1] = progressBar2;
            _progressBars[2] = progressBar3;

            for (int i = 0; i < 3; i++)
            {
                _progressBars[i].Minimum = 0;
                _progressBars[i].Maximum = MaxValue;
                _progressBars[i].Style = ProgressBarStyle.Continuous;
                _progressBars[i].Value = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                if (_threads[i] != null)
                {
                    _threads[i].Abort();
                }
                _threads[i] = new Thread(new ParameterizedThreadStart(ThreadProc));
                _threads[i].IsBackground = true;
                _threads[i].Start(i);
            }

        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            if (_manual)
            {
                _manualEvent.Set();
            }
            else
            {
                _autoEvent.Set();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (_manual)
            {
                _manualEvent.Reset();
            }
            else
            {
                _autoEvent.Reset();
            }
        }

        private void ThreadProc(object state)
        {
            int threadNumber = (int)state;
            int value = 0;
            while (value < MaxValue)
            {
                if (_manual)
                {
                    _manualEvent.WaitOne();
                }
                else
                {
                    _autoEvent.WaitOne();
                }
                for (int i = 0; i < 100000; ++i)
                {
                    ++value;
                    UpdateProgress(threadNumber, value);
                    //just so we don't completely peg the CPU at 100%
                    Thread.Sleep(0);
                }
            }
        }

        private void UpdateProgress(int thread, int value)
        {
            //must invoke because these must be updated on UI thread
            _progressBars[thread].Invoke(new MethodInvoker(delegate
            {
                _progressBars[thread].Value = value;
            }));
        }

        private void OnEventTypeChanged(object sender, EventArgs e)
        {
            _manual = radioButtonManual.Checked;
            buttonReset.Enabled = _manual;
            Init();
        }
    }
}
