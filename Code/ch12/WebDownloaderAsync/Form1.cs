using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace WebDownloaderAsync
{
    public partial class Form1 : Form
    {
        WebClient _client = null;
        bool _downloading = false;//for tracking what button does

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (!_downloading)
            {
                _client = new WebClient();
                //listen for events so we know when things happen
                _client.DownloadProgressChanged += _client_DownloadProgressChanged;
                _client.DownloadDataCompleted += _client_DownloadDataCompleted;

                try
                {
                    //start downloading and immediately return
                    _client.DownloadDataAsync(new Uri(textBoxUrl.Text));
                    //now our program an do other stuff while we wait!
                    _downloading = true;
                    buttonDownload.Text = "Cancel";
                }
                catch (UriFormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    _client.Dispose();
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                    _client.Dispose();
                }
            }
            else
            {
                _client.CancelAsync();
            }
        }
        
        void _client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            labelStatus.Text = string.Format("{0:N0} / {1:N0} bytes received", e.BytesReceived, e.TotalBytesToReceive);
        }

        void _client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            //now the file is done downloading
            if (e.Cancelled)
            {
                progressBar.Value = 0;
                labelStatus.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                progressBar.Value = 0;
                labelStatus.Text = e.Error.Message;
            }
            else
            {
                progressBar.Value = 100;
                labelStatus.Text = "Done!";
             }
            _client.Dispose();
            _downloading = false;
            buttonDownload.Text = "Download";
            //access data in e.Result
        }
    }
}