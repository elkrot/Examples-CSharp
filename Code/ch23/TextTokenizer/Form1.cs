using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TextTokenizer
{
    public partial class Form1 : Form
    {
        byte[] _buffer;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            UpdateProgress("Reading file");
            FileStream inputStream = new FileStream(textBoxUrl.Text,FileMode.Open);
            _buffer = new byte[inputStream.Length];
            //pass in the inputStream as the argument to the "Done" method
            IAsyncResult result = inputStream.BeginRead(_buffer, 0, _buffer.Length, FileReadDone, inputStream);
            //the IAsyncResult object can be used to track the progress of the method

            //while the file reading is going on, we can do other work, like click buttons or exit the program
        }

        private void FileReadDone(IAsyncResult result)
        {
            UpdateProgress("File read done");
            FileStream inputStream = result.AsyncState as FileStream;
            inputStream.Close();

            //start async tokenizing
            TokenCounter counter = new TokenCounter(Encoding.ASCII.GetString(_buffer));
            IAsyncResult counterResult = counter.BeginCount(CountDone, counter);
            UpdateProgress("Counting tokens");
            //if we want to wait for it to finish, call:
            //counter.EndCount(counterResult);
        }

        private void CountDone(IAsyncResult result)
        {
            UpdateProgress("Count done");
            TokenCounter counter = result.AsyncState as TokenCounter;
            
            listViewTokens.Invoke(new MethodInvoker(delegate
            {
                listViewTokens.BeginUpdate();
                foreach (WordCount count in counter.WordCounts)
                {
                    ListViewItem item = new ListViewItem(count.Word);
                    item.SubItems.Add(count.Count.ToString("N0"));
                    listViewTokens.Items.Add(item);
                }
                listViewTokens.EndUpdate();
            }));
        }

        private void UpdateProgress(string message)
        {
            this.BeginInvoke(new MethodInvoker(delegate
                {
                    this.labelStatus.Text = message;
                }));
        }

    }
}
