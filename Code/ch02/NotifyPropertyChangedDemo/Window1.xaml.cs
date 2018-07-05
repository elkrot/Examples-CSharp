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
using System.Windows.Threading;

namespace NotifyPropertyChangedDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DispatcherTimer timer;
        private Random rand = new Random();
        private MyDataClass data = new MyDataClass();

        public Window1()
        {
            InitializeComponent();

            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, OnTimer, this.Dispatcher);
            
            //tell WPF to look for data bindings in this object by default
            //WPF will use the INotifyPropertyChanged to know when to update UI
            this.DataContext = data;
            data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(data_PropertyChanged);
        }

        void data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //do something when data changes
        }

        private void OnTimer(object sender, EventArgs e)
        {
            //at no point do we explicitly set
            data.Tag = rand.Next();
        }
    }
}
