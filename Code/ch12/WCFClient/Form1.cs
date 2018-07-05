using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCFClient
{
    public partial class Form1 : Form
    {
        FileServiceClient fsClient = null;

        public Form1()
        {
            InitializeComponent();

            fsClient = new FileServiceClient();
        }

        private void buttonGetSubDirs_Click(object sender, EventArgs e)
        {
            SetResults(fsClient.GetSubDirectories(textBoxGetSubDirs.Text));
        }
        
        private void buttonGetFiles_Click(object sender, EventArgs e)
        {
            SetResults(fsClient.GetFiles(textBoxGetFiles.Text));
        }

        private void buttonGetFileContents_Click(object sender, EventArgs e)
        {
            int bytesToRead = (int)numericUpDownBytesToRead.Value;
            byte[] buffer = new byte[bytesToRead];
            int bytesRead = fsClient.RetrieveFile(out buffer, textBoxRetrieveFile.Text, bytesToRead);

            if (bytesRead > 0)
            {
                //just assume ASCII for this example
                string text = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                SetResults(text);
            }
        }

        private void SetResults(string[] results)
        {
            //use LINQ to concat the results easily
            textBoxOutput.Text = results.Aggregate((a, b) => a + Environment.NewLine + b);
        }

        private void SetResults(string results)
        {
            textBoxOutput.Text = results;
        }
    }
}
