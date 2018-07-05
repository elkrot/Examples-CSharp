using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace VideoPlayer
{
    public partial class PlayDownloadProgressControl : UserControl
    {
        public PlayDownloadProgressControl()
        {
            InitializeComponent();
        }

        public void UpdatePlaybackProgress(double playbackPercent)
        {
            
            PlaybackProgress.X2 = PlaybackProgress.X1 + (playbackPercent * LayoutRoot.ActualWidth / 100.0);
        }

        public void UpdateDownloadProgress(double downloadPercent)
        {
            DownloadProgress.X2 = DownloadProgress.X1 + (downloadPercent * LayoutRoot.ActualWidth / 100.0);
        }
    }
}
