using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace VideoPlayer
{
    public partial class MainPage : UserControl
    {
        private static DependencyProperty IsPlayingProperty;
        
        static MainPage()
        {
            IsPlayingProperty = DependencyProperty.Register("IsPlaying", typeof(bool), typeof(MainPage),
                new PropertyMetadata(false));
        }

        public bool IsPlaying
        {
            get
            {
                return (bool)GetValue(IsPlayingProperty);
            }
            set
            {
                SetValue(IsPlayingProperty, value);
            }
        }

        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public MainPage()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }

       
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPlaying)
            {
                //changing the source needlessly causes the video to reset
                if (videoPlayer.Source == null || videoPlayer.Source.OriginalString != textBoxURL.Text)
                {
                    videoPlayer.Source = new Uri(textBoxURL.Text);
                }
                videoPlayer.Play();
                IsPlaying = true;
                //it may be better to use a value converter to set button text
                //but that would distract from the essential part of this example
                buttonPlay.Content = "Pause";
            }
            else if (videoPlayer.CanPause)
            {
                videoPlayer.Pause();
                IsPlaying = false;
                buttonPlay.Content = "Play";
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
            IsPlaying = false;
            buttonPlay.Content = "Play";
        }

        private void videoPlayer_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            progressBar.UpdateDownloadProgress(100.0 * videoPlayer.DownloadProgress);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            switch (videoPlayer.CurrentState)
            {
                case MediaElementState.Playing:
                case MediaElementState.Buffering:
                    if (videoPlayer.NaturalDuration.HasTimeSpan)
                    {
                        double total = videoPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                        if (total > 0.0)
                        {
                            double elapsed = videoPlayer.Position.TotalMilliseconds;
                            progressBar.UpdatePlaybackProgress(100.0 * elapsed / total);
                        }
                    }
                    break;
                default:
                    //do nothing
                    break;
            }
        }
    }
}
