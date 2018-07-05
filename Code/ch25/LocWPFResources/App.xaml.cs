using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;

namespace LocWPFResources
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //set UI to have whatever be same as non-UI, which is what is in Control Panel
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            //LocWPFResources.Properties.Resources.Culture = Thread.CurrentThread.CurrentCulture;
        }
    }
}
