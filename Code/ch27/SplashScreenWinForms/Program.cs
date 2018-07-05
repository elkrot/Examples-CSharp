using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace SplashScreenWinForms
{
    static class Program
    {
        private static Thread _loadThread;
        private static SplashScreen _splash;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _splash = new SplashScreen(Properties.Resources.splash);

            _loadThread = new Thread((ThreadStart)delegate
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
                    Win32.PlaySound(".Default", 0, (int)(Win32.Soundflags.SND_ALIAS | Win32.Soundflags.SND_ASYNC | Win32.Soundflags.SND_NOWAIT));
                    _splash.Invoke((MethodInvoker)delegate { _splash.Close(); });
                });
            _loadThread.Start();

            
            _splash.TopLevel = true;
            _splash.ShowDialog();

            Application.Run(new Form1());
        }

       
    }
}
