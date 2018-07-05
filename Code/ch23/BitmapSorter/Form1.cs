using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BitmapSorter
{
    public partial class Form1 : Form
    {
        private ScrambledBitmap scrambledBitmap;
        private Bitmap displayBitmap;
        private System.Threading.Timer updateTimer;

        public Form1()
        {
            InitializeComponent();

            OpenBitmap("Elements_Small.png");
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                OpenBitmap(e.Data.GetData(DataFormats.FileDrop) as string);
            }
        }

        private void OpenBitmap(string filename)
        {
            Bitmap bitmap = new Bitmap(filename);
            scrambledBitmap = new ScrambledBitmap(bitmap);
            SetDisplayBitmap(bitmap);
        }

        private void buttonScrambleSort_Click(object sender, EventArgs e)
        {
            if (scrambledBitmap.IsSorted)
            {
                scrambledBitmap.Scramble();
                buttonScrambleSort.Text = "&Sort";
                SetDisplayBitmap(scrambledBitmap.ToBitmap());
                updateTimer = new System.Threading.Timer(UpdateBitmapCallback, null, 1000, 1000);
                
            }
            else
            {
                buttonScrambleSort.Enabled = false;
                buttonScrambleSort.Text = "&Scramble";
                //use the threadpool to do our sort
                ThreadPool.QueueUserWorkItem(delegate{
                    scrambledBitmap.Sort();
                });
            }
        }

        private void UpdateBitmapCallback(object state)
        {
            SetDisplayBitmap(scrambledBitmap.ToBitmap());
            if (scrambledBitmap.IsSorted)
            {
                updateTimer.Dispose();
                //update UI on main thread
                Invoke(new MethodInvoker(
                    () =>
                    {
                        buttonScrambleSort.Enabled = true;
                    })
                );
                
            }
        }

        private void SetDisplayBitmap(Bitmap newBitmap)
        {
            if (displayBitmap != null)
            {
                displayBitmap.Dispose();
            }
            displayBitmap = newBitmap;
            pictureBox.Image = displayBitmap;
        }
    }
}
