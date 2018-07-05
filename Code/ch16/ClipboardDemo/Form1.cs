using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClipboardDemo
{
    public partial class Form1 : Form
    {
        Random _rand = new Random();
        string[][] _people = { 
            new string[] {"Bob","M","45"},
            new string[] {"Mary","F","24"},
            new string[] {"Bill","M","25"},
            new string[] {"Mark","M","37"},
            new string[] {"Ashley","F","39"},
            new string[] {"Anna","F","56"},
            new string[] {"Joe","M","65"}
                             };
        public Form1()
        {
            InitializeComponent();

            listView1.FullRowSelect = true;
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);

            AddRandomPerson();
            AddRandomPerson();
            AddRandomPerson();

            listView1.Items[0].Selected = true;
        }

        void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCopy.Enabled = buttonCut.Enabled = listView1.SelectedIndices.Count > 0;
        }

        private void AddRandomPerson()
        {
            string[] person = _people[_rand.Next(0,_people.Length-1)];
            AddPerson(person[0], person[1], person[2]);
        }

        private void AddPerson(string name, string sex, string age)
        {
            ListViewItem item = new ListViewItem(name);
            item.SubItems.Add(sex);
            item.SubItems.Add(age);

            listView1.Items.Add(item);
        }

        private void buttonAddRandomPerson_Click(object sender, EventArgs e)
        {
            AddRandomPerson();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            CopyAllFormats();
        }

        private void CopyAllFormats()
        {
            DataObject obj = new DataObject();
            obj.SetText(GetText());
            obj.SetImage(GetBitmap());
            obj.SetData(MyClipboardItem.Format.Name, RowsToClipboardItems());
            Clipboard.SetDataObject(obj);

            Image image = Clipboard.GetImage();
            string text = Clipboard.GetText();
        }

        private string GetText()
        {
            StringBuilder sb = new StringBuilder(256);
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                sb.AppendFormat("{0},{1},{2}", item.Text, item.SubItems[1].Text, item.SubItems[2].Text);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(listView1.Width, listView1.Height);
            listView1.DrawToBitmap(bitmap, listView1.ClientRectangle);
            return bitmap;
        }

       
        private List<MyClipboardItem> RowsToClipboardItems()
        {
            List<MyClipboardItem> clipItems = new List<MyClipboardItem>();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                clipItems.Add(
                    new MyClipboardItem(item.Text, item.SubItems[1].Text, item.SubItems[2].Text)
                    );
            }
            return clipItems;
        }

        private void buttonPasteToList_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData(MyClipboardItem.Format.Name))
            {
                IList<MyClipboardItem> items = GetItemsFromClipboard();
                foreach (MyClipboardItem item in items)
                {
                    AddPerson(item.Name, item.Sex, item.Age);
                }
            }
            else
            {
                MessageBox.Show("Nothing on the clipboard in the right format!");
            }
        }

        IList<MyClipboardItem> GetItemsFromClipboard()
        {
            object obj = Clipboard.GetData(MyClipboardItem.Format.Name);
            return obj as IList<MyClipboardItem>;
        }

        private void buttonCut_Click(object sender, EventArgs e)
        {
            CopyAllFormats();

            List<ListViewItem> itemsToDelete = new List<ListViewItem>();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                itemsToDelete.Add(item);
            }
            foreach (ListViewItem item in itemsToDelete)
            {
                item.Remove();
            }
        }

        
    }
}
