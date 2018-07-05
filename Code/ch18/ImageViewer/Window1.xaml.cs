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
using System.Windows.Media.Animation;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static DependencyProperty ImageInfoProperty;
        static Window1()
        {
            ImageInfoProperty = DependencyProperty.Register("ImageInfo",
                typeof(ImageInfoViewModel), typeof(Window1));
        }

        public ImageInfoViewModel ImageInfo
        {
            get
            {
                return (ImageInfoViewModel)GetValue(ImageInfoProperty);
            }
            set
            {
                SetValue(ImageInfoProperty, value);
                DataContext = value;
            }
        }

        public Window1()
        {
            InitializeComponent();
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            //is it a list of files?
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach(string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    //grab first image
                    try
                    {
                        BitmapImage image = new BitmapImage(new Uri(path));
                        ImageInfoViewModel model = new ImageInfoViewModel(image);
                        this.ImageInfo = model;
                    }
                    catch (Exception )
                    {
                    }
                }
            }
        }

        private void OnTemplateOptionChecked(object sender, RoutedEventArgs e)
        {
            if (radioButtonNoCaption != null && controlDisplay!= null)
            {
                if (radioButtonNoCaption.IsChecked == true)
                {
                    controlDisplay.Template = (ControlTemplate)FindResource("imageTemplate");
                }
                else
                {
                    controlDisplay.Template = (ControlTemplate)FindResource("imageWithCaptionTemplate");
                }
            }
        }

        
    }
}
