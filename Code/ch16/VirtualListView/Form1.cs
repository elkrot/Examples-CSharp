using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualListViewSort
{
    public partial class Form1 : Form
    {
        private List<ListViewItem> _listViewItemCache = new List<ListViewItem>();
        private int _topIndex = -1;

        public Form1()
        {
            InitializeComponent();

            listView.VirtualMode = true;
            listView.VirtualListSize = (int)numericUpDown1.Value;
            listView.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView_CacheVirtualItems);
            listView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);
        }
        
        void listView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            _topIndex = e.StartIndex;
            //find out if we need more items
            //(note we never make the list smaller--not a very good cache)
            int needed = (e.EndIndex - e.StartIndex) + 1;
            if (_listViewItemCache.Capacity < needed)
            {
                int toGrow = needed - _listViewItemCache.Capacity;
                //adjust the capacity to the target
                _listViewItemCache.Capacity = needed;
                //add the new cached items
                for (int i = 0; i < toGrow; i++)
                {
                    _listViewItemCache.Add(new ListViewItem());
                }
            }            
        }
               

        void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            int cacheIndex = e.ItemIndex - _topIndex;
            if (cacheIndex >= 0 && cacheIndex < _listViewItemCache.Count)
            {
                e.Item = _listViewItemCache[cacheIndex];
                //we could set the text to any data we want, bsed on e.ItemIndex
                //let's just show the item index and the cache index for simplicity
                e.Item.Text = e.ItemIndex.ToString() + " --> " + cacheIndex.ToString();
                //e.Item.Tag = some arbitrary object                        
            }
            else
            {
                //this can happen occasionally, but you won't see it
                e.Item = _listViewItemCache[0];
                e.Item.Text = "Oops";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            listView.VirtualListSize = (int)numericUpDown1.Value;
        }
    }
}
