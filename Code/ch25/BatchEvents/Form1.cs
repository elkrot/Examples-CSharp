using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatchEvents
{
    public partial class Form1 : Form
    {
        MyCollection<int> _items = new MyCollection<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOneAtATime_Click(object sender, EventArgs e)
        {
            _items = new MyCollection<int>();
            _items.ItemsAdded += new EventHandler<ItemAddedEventArgs<int>>(_items_ItemsAdded);
            
            GenerateItems();
        }

        private void buttonUpdateBatch_Click(object sender, EventArgs e)
        {
            _items = new MyCollection<int>();
            _items.ItemsAdded += new EventHandler<ItemAddedEventArgs<int>>(_items_ItemsAdded);

            _items.BeginUpdate();
            GenerateItems();
            _items.EndUpdate();
        }

        private void GenerateItems()
        {
            listViewOutput.Items.Clear();

            DateTime start = DateTime.Now;
            for (int i = 0; i < 20000; i++)
            {
                _items.Add(i);
            }
            DateTime end = DateTime.Now;
            TimeSpan diff = end - start;
            labelElapsed.Text = diff.ToString();
        }

        void _items_ItemsAdded(object sender, ItemAddedEventArgs<int> e)
        {
            listViewOutput.BeginUpdate();
            foreach (var i in e.Items)
            {
                listViewOutput.Items.Add(i.ToString());
            }
            listViewOutput.EndUpdate();
        }
    }
}
