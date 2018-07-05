using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace BitmapSorter
{
    class BitmapLine
    {
        public int Index { get; private set; }
        public Color[] Pixels { get; private set; }
        public BitmapLine(int index, Color[] pixels)
        {
            this.Index = index;
            this.Pixels = pixels;
        }
    }
    class ScrambledBitmap
    {
        BitmapLine[] _lines;
        
        static Random rand = new Random();

        object syncObj = new object();

        public bool IsSorted { get; private set; }

        public ScrambledBitmap(Bitmap image)
        {
            _lines = new BitmapLine[image.Height];
            for (int row = 0; row < image.Height; row++)
            {
                _lines[row] = new BitmapLine(row, new Color[image.Width]);
                for (int col = 0; col < image.Width; col++)
                {
                    _lines[row].Pixels[col] = image.GetPixel(col, row);
                }
            }
            IsSorted = true;
        }

        public void Scramble()
        {
            for (int row = 0; row < _lines.Length; row++)
            {
                int targetRow = rand.Next(0, _lines.Length);
                SwapLines(row, targetRow);
            }
            IsSorted = false;
        }

        private void SwapLines(int i, int j)
        {
            BitmapLine temp = _lines[j];
            _lines[j] = _lines[i];
            _lines[i] = temp;
        }

        public void Sort()
        {
            //do a particularly slow bubble sort to demonstrate multithreading aspects
            bool changeMade = true;
            while (changeMade)
            {
                lock (syncObj)
                {
                    changeMade = false;
                    for (int i = 0; i < _lines.Length - 1; i++)
                    {
                        if (_lines[i].Index > _lines[i + 1].Index)
                        {
                            changeMade = true;
                            SwapLines(i, i + 1);
                        }
                    }
                } 
                Thread.Sleep(50);
            }
            IsSorted = true;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(_lines[0].Pixels.Length, _lines.Length);
            lock (syncObj)
            {
                for (int row = 0; row < bmp.Height; row++)
                {
                    for (int col = 0; col < bmp.Width; col++)
                    {
                        bmp.SetPixel(col, row, _lines[row].Pixels[col]);
                    }
                }
            }
            return bmp;
        }
    }
}
