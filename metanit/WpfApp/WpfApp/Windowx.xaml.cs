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

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для Windowx.xaml
    /// </summary>
    public partial class Windowx : Window
    {
        public Windowx()
        {
            InitializeComponent();
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }
    }
}
