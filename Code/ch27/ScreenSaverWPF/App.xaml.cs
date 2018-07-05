using System;
using System.Windows;

namespace ScreenSaverWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length >= 1)
            {
                if (string.Compare("/c", e.Args[0], true) == 0)
                {
                    //show config
                    OptionsWindow window = new OptionsWindow();
                    window.ShowDialog();
                }
                else if (string.Compare("/p", e.Args[0], true) == 0 && e.Args.Length >= 2)
                {
                    //preview screen saver inside an existing window

                    //next arg is a window handle 
                    int handle = 0;
                    if (int.TryParse(e.Args[1], out handle))
                    {
                        IntPtr ptr = new IntPtr(handle);
                        Win32.RECT rect;
                        if (Win32.GetWindowRect(ptr, out rect))
                        {
                            //rather than inserting the WPF window into the existing window,
                            //just put our window in the same place
                            ScreenSaverWindow previewWindow = new ScreenSaverWindow(new Point(rect.Left, rect.Top), new Size(rect.Width, rect.Height));
                            previewWindow.ShowInTaskbar = false;
                            previewWindow.Show();

                            //important to stop preview
                            previewWindow.CaptureMouse();
                            return;
                        }
                    }
                }
                else if (string.Compare("/s", e.Args[0], true) == 0)
                {
                    //only run screen saver when /s is passed
                    ScreenSaverWindow screenWindow = new ScreenSaverWindow();
                    screenWindow.Show();
                    return;
                }
            }
            //shutdown on any errors and when config dialog is closed
            Application.Current.Shutdown();            
        }
    }
}
