using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DrawingDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            radioButtonSmoothingAntiAlias.Tag = SmoothingMode.AntiAlias;
            radioButtonSmoothingDefault.Tag = SmoothingMode.Default;
            radioButtonSmoothingHighQuality.Tag = SmoothingMode.HighQuality;
            radioButtonSmoothingHighSpeed.Tag = SmoothingMode.HighSpeed;
            radioButtonSmoothingNone.Tag = SmoothingMode.None;

            radioButtonTextSmoothingAntiAlias.Tag = System.Drawing.Text.TextRenderingHint.AntiAlias;
            radioButtonTextSmoothingAntiAliasGridFit.Tag = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            radioButtonTextSmoothingClearTypeGridFit.Tag = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            radioButtonTextSmoothingDefault.Tag = System.Drawing.Text.TextRenderingHint.SystemDefault;
            radioButtonTextSmoothingSingleBitPerPixel.Tag = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            radioButtonTextSmoothingSingleBitPerPixelGridFit.Tag = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
        }

        private void OnSmoothingChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button != null)
            {
                drawingPanel.SmoothingMode = (SmoothingMode)button.Tag;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = drawingPanel.TextFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                drawingPanel.TextFont = fd.Font;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            drawingPanel.TextAngle = (int)numericUpDown1.Value;
        }

        private void OnTextRenderingChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button!=null)
            {
                drawingPanel.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)button.Tag;
            }
        }

        
    }
}
