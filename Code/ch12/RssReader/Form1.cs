using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RssReader
{
    public partial class Form1 : Form
    {
        RssLib.Feed _feed = new RssLib.Feed();

        public Form1()
        {
            InitializeComponent();

            this.textBoxFeed.Text = @"http://feeds.feedburner.com/PhilosophicalGeek";
            LoadFeed(textBoxFeed.Text);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadFeed(textBoxFeed.Text);
        }

        private void LoadFeed(string url)
        {
            listViewEntries.Items.Clear();

            RssLib.Channel channel = _feed.Read(url);
            this.Text = "RSS Reader - " + channel.Title;
            
            foreach (RssLib.Item item in channel.Items)
            {
                ListViewItem listViewItem = new ListViewItem(item.PubDate.ToString());
                listViewItem.SubItems.Add(item.Title);
                listViewItem.SubItems.Add(item.Link);
                listViewItem.Tag = item;
                listViewEntries.Items.Add(listViewItem);
            }
        }

        private void OnSelectListViewItem(object sender, EventArgs e)
        {
            if (listViewEntries.SelectedItems.Count > 0)
            {
                textBoxDescription.Text = (listViewEntries.SelectedItems[0].Tag as RssLib.Item).Description;
            }
            else
            {
                textBoxDescription.Text = "";
            }
        }
    }
}
