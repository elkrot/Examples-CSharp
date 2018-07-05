using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;

namespace SplashScreenWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SplashScreen _splash = new SplashScreen();
        private delegate void WPFMethodInvoker();
        Thread _thread;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Thread thread = new Thread((ThreadStart)delegate
            {
                //do loading tasks
                string[] fakeLoadingTasks = new string[]
                    {
                        "Loading greebles",
                        "Refactoring image levels",
                        "Doodling",
                        "Adding dogs and cats",
                        "Catmulling curves",
                        "Taking longer just because"
                    };

                for (int i = 0; i < fakeLoadingTasks.Length; i++)
                {
                    if (_splash != null)
                    {
                        _splash.UpdateStatus(fakeLoadingTasks[i], 100 * i / fakeLoadingTasks.Length);
                    }
                    Thread.Sleep(2000);
                }
                
                _splash.Dispatcher.Invoke((WPFMethodInvoker)delegate { _splash.Close(); });
            });
            thread.Start();
            _splash.Show();
        }
    }
}
