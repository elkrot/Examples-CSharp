using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace SplashScreenWPF
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private delegate void UpdateStatusDelegate(string status, int percent);
        private UpdateStatusDelegate _updateDelegate;

        public SplashScreen()
        {
            InitializeComponent();
            _updateDelegate = UpdateStatus;
        }

        public void UpdateStatus(string status, int percent)
        {
            if (Dispatcher.Thread.ManagedThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                this.Dispatcher.Invoke(_updateDelegate, status, percent);
            }
            else
            {
                statusLabel.Content = status;
                progressBar.Value = percent;
            }
        }
    }
}
