using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace MVVMDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindowViewModel _mainViewModel = new MainWindowViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            

            _mainViewModel.Close += new EventHandler<EventArgs>(delegate
                {
                    Shutdown();
                });

            MainWindow window = new MainWindow();

            MainWindow.DataContext = _mainViewModel;

            window.Show();
        }
    }
}
