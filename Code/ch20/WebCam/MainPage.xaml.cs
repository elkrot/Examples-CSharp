using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WebCam
{
    public partial class MainPage : UserControl
    {
        CaptureSource _captureSource = null;

        public MainPage()
        {
            InitializeComponent();
            
        }

        void StartWebcam()
        {
            if (_captureSource != null && 
                _captureSource.State != CaptureState.Started)
            {
                if (CaptureDeviceConfiguration.AllowedDeviceAccess
                    || CaptureDeviceConfiguration.RequestDeviceAccess())
                {
                    VideoCaptureDevice device = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
                    if (device != null)
                    {
                        _captureSource = new CaptureSource();
                        _captureSource.VideoCaptureDevice = device;
                        _captureSource.Start();
                        VideoBrush brush = new VideoBrush();
                        brush.Stretch = Stretch.Uniform;
                        brush.SetSource(_captureSource);
                        videoRect.Fill = brush;
                    }
                }
            }
        }

        private void buttonStartWebcam_Click(object sender, RoutedEventArgs e)
        {
            StartWebcam();
        }
    }
}
