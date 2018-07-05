using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Threading;

namespace ScreenSaverWPF
{
    public partial class ScreenSaverWindow : Window
    {
        FileInfo[] _images;
        int _imageIndex = 0;
        DispatcherTimer _timer;
        private Point _prevPt ;
        bool _trackingMouse = false;
        Random _rand = new Random();

        //full-screen constructor
        public ScreenSaverWindow()
        {
            InitializeComponent();

            SetFullScreen();

            Initialize();          
        }

        //constructor for preview window
        public ScreenSaverWindow(Point point, Size size)
        {
            InitializeComponent();

            SetWindowSize(point, size);

            Initialize();
        }

        private void Initialize()
        {
            LoadImages();

            //timer to change image every 5 seconds
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 5);
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();
        }

        private void LoadImages()
        {
            //if you have a lot of images, this could take a while...
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            _images = directoryInfo.GetFiles("*.jpg", SearchOption.AllDirectories);
        }

        private void SetFullScreen()
        {
            //get a rectangle representing all the screens
            //alternatively, you could just have separate windows for each monitor
            System.Drawing.Rectangle fullScreen = new System.Drawing.Rectangle(0,0,0,0);
            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                fullScreen = System.Drawing.Rectangle.Union(fullScreen, screen.Bounds);
            }
            this.Left = fullScreen.Left;
            this.Top = fullScreen.Top;
            this.Width = fullScreen.Width;
            this.Height = fullScreen.Height;
        }

        private void SetWindowSize(Point point, Size size)
        {
            this.Left = point.X;
            this.Top = point.Y;
            this.Width = size.Width;
            this.Height = size.Height;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (_images!=null && _images.Length > 0)
            {
                ++_imageIndex;
                _imageIndex = _imageIndex % _images.Length;

                this.imageFloating.Source = new BitmapImage(new Uri(_images[_imageIndex].FullName));
                MoveToRandomLocation(imageFloating);
                Size size = GetImageSize(imageFloating);
                imageFloating.Width = size.Width;
                imageFloating.Height = size.Height;
            }
        }

        private void MoveToRandomLocation(Image imageFloating)
        {
            double x = _rand.NextDouble() * canvas.ActualWidth / 2;
            double y = _rand.NextDouble() * canvas.ActualHeight / 2;

            Canvas.SetLeft(imageFloating, x);
            Canvas.SetTop(imageFloating, y);
        }

        private Size GetImageSize(Image image)
        {
            //this overly-simple algorithm won't work for the preview window
            double ratio = image.Source.Width / image.Source.Height;
            double width = 0;
            double height = 0;
            if (ratio > 1.0)
            {
                //wider than is tall
                width = 1024;
                height = width / ratio;
            }
            else
            {
                height = 1024;
                width = height * ratio;
            }
            return new Size(width, height);
        }

        //end screen saver
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            Application.Current.Shutdown();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point location = e.MouseDevice.GetPosition(this);
            //use _trackingMouse to know when we've got a previous point to compare to
            if (_trackingMouse)
            {
                //only end if the mouse has moved enough
                if (Math.Abs(location.X - _prevPt.X) > 10 || Math.Abs(location.Y - _prevPt.Y) > 10)
                {
                    Application.Current.Shutdown();
                }
            }
            _trackingMouse = true;
            _prevPt = location;            
        }
    }
}
