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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnhandledExceptionWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Application.Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Current_DispatcherUnhandledException);
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Caught unhandled exception");
            sb.AppendLine(e.Exception.ToString());

            MessageBox.Show(sb.ToString());

            e.Handled = true;//prevent exit
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("Oops");
        }
    }
}
