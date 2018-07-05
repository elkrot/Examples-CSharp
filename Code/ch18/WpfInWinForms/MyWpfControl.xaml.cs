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

namespace WpfInWinForms
{
    /// <summary>
    /// Interaction logic for MyWpfControl.xaml
    /// </summary>
    public partial class MyWpfControl : UserControl
    {
        public event EventHandler<EventArgs> ButtonClicked;

        public MyWpfControl()
        {
            InitializeComponent();
        }

        private void OnClick(object source, RoutedEventArgs e)
        {
            //surface the button event as a control event
            if (ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
        }


    }
}
