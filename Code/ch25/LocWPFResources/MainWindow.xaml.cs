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
using System.IO;

namespace LocWPFResources
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //set the image
            imageFlag.BeginInit();
            imageFlag.Source = CreateImageSource(Properties.Resources.flag);
            imageFlag.EndInit();
        }

        private void OnClick_Exit(object sender, RoutedEventArgs e)
        {
            //look up message from resources

            string message = Properties.Resources.messageExit;
            if (MessageBox.Show(message, this.Title, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private ImageSource CreateImageSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                //the OnLoad option uses the stream immediately so that we can dispose it
                return BitmapFrame.Create(stream,BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

    }
}
