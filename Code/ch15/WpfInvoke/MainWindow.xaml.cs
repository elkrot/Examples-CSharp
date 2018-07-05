using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace WpfInvoke
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //startup second thread to do the updating
            Thread thread = new Thread(new ThreadStart(ThreadProc));
            thread.IsBackground = true;
            thread.Start();
        }

        private void ThreadProc()
        {
            int val = 0;
            while (true)
            {
                ++val;
                UpdateValue(val);
                Thread.Sleep(200);
            }
        }

        private delegate void UpdateValueDelegate(int val);

        private void UpdateValue(int val)
        {
            if (Dispatcher.Thread != Thread.CurrentThread)
            {
                Dispatcher.Invoke(new UpdateValueDelegate(UpdateValue), val);
            }
            else
            {
                textBlock.Text = val.ToString("N0");
            }

        }
    }
}
