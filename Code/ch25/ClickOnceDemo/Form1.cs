using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.IsolatedStorage;

namespace ClickOnceDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonWritetoFS_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(@"C:\Hello.txt", "Hello, ClickOnce");
                MessageBox.Show("Wrote to file succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonWriteToIS_Click(object sender, EventArgs e)
        {
            try {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = store.CreateFile(@"Hello.txt"))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write("Hello, clickOnce");
                        MessageBox.Show("Wrote to Isolate storage succesfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
