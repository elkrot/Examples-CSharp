using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SemaphoreDemo
{
    public partial class Form1 : Form
    {
        //allow two concurrent entires (with a max of 2)
        //the two values allow you to start with some of them locked
        //if needed
        Semaphore _semaphore = new Semaphore(2, 2);
        private ProgressBar[] _progressBars = new ProgressBar[3];
        private Thread[] _threads = new Thread[3];
        
        const int MaxValue = 1000000;

        public Form1()
        {
            InitializeComponent();

            _progressBars[0] = progressBar1;
            _progressBars[1] = progressBar2;
            _progressBars[2] = progressBar3;

            for (int i = 0; i < 3; i++)
            {
                _progressBars[i].Minimum = 0;
                _progressBars[i].Maximum = MaxValue;
                _progressBars[i].Style = ProgressBarStyle.Continuous;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            for (int i = 0; i < 3; i++)
            {
                _threads[i] = new Thread(new ParameterizedThreadStart(IncrementThread));
                _threads[i].IsBackground = true;
                _threads[i].Start(i);
            }
        }

        private void IncrementThread(object state)
        {
            int threadNumber = (int)state;
            int value = 0;
            while (value < MaxValue)
            {
                //only two threads at a time will be 
                //allowed to enter this section
                _semaphore.WaitOne();
                for (int i = 0; i < 100000; i++)
                {
                    ++value;
                    UpdateProgress(threadNumber, value);
                }
                _semaphore.Release();
            }
        }

        private void UpdateProgress(int thread, int value)
        {
            if (value <= MaxValue)
            {
                //must invoke because these must be updated on UI thread
                _progressBars[thread].Invoke(new MethodInvoker(delegate
                {
                    _progressBars[thread].Value = value;
                }));
            }
        }
        
        private void ThreadProc()
        {
            int value = 13;

            Interlocked.Increment(ref value);//adds one
            Interlocked.Decrement(ref value);//subtracts one
            Interlocked.Add(ref value, 13);//adds 13
            int originalValue = Interlocked.Exchange(ref value, 99);//set value to 99 and return the original value
            string s1 = "Hello";
            string sNew = "Bonjour";
            string sCompare = "Hello";
            string sOriginal = Interlocked.CompareExchange<string>(ref s1, sNew, sCompare);
        }
    }
}
