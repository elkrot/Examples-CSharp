using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Net.Mail;
using System.Net;

namespace EmailClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    ListViewItem item = new ListViewItem(file);
                    FileInfo info = new FileInfo(file);
                    item.SubItems.Add(string.Format("{0:N0}KB", info.Length / 1024));
                    listViewFiles.Items.Add(item);
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (listViewFiles.SelectedItems.Count > 0)
                    {
                        List<ListViewItem> deletedItems = new List<ListViewItem>();
                        foreach (ListViewItem item in listViewFiles.SelectedItems)
                        {
                            deletedItems.Add(item);
                        }
                        
                        foreach (ListViewItem item in deletedItems)
                        {
                            item.Remove();
                        }
                    }
                    break;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            List<string> attachments = new List<string>();
            foreach (ListViewItem item in listViewFiles.Items)
            {
                attachments.Add(item.Text);
            }

            try
            {
                SendEmail(textBoxHostname.Text, (int)numericUpDownPort.Value,
                    textBoxUsername.Text, textBoxPassword.Text,
                    textBoxFrom.Text, textBoxTo.Text,
                    textBoxSubject.Text, textBoxBody.Text,
                    attachments);
                MessageBox.Show("Message sent");
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }

            
        }

        private void SendEmail(string host, int port,
            string username, string password,
            string from, string to,
            string subject, string body,
            ICollection<string> attachedFiles)
        {
            //A MailMessage object must be disposed!
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(from);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                foreach (string file in attachedFiles)
                {
                    message.Attachments.Add(new Attachment(file));
                }

                SmtpClient client = new SmtpClient(host, port);
                //if your SMTP server requires a password, this line is important
                client.Credentials = new NetworkCredential(username, password);
                //this send is syncronous. You can also choose to send asyncronously
                client.Send(message);
            }
        }
    }
}
