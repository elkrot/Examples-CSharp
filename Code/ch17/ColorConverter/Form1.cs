using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorConverter
{
    public partial class Form1 : Form
    {
        bool _updating = false;

        public Form1()
        {
            InitializeComponent();

            SetColorRanges();

            SetupHueColors();
        }

        private void SetupHueColors()
        {
            //we want hue slider to be a little different than others
            Color[] colors = new Color[360];
            int h = 0, s = 100, v = 100;
            for (h = 0; h < colors.Length; h++)
            {
                colors[h] = Converter.HsvToRgb(h, s, v);                
            }
            colorSliderH.CustomColors = colors;
        }

        private void SetColorRanges()
        {
            Color rgbColor = GetRgbColor();

            //RGB sliders
            colorSliderR.SetColors(
                Color.FromArgb(0, rgbColor.G, rgbColor.B),
                Color.FromArgb(255, rgbColor.G, rgbColor.B));

            colorSliderG.SetColors(
                Color.FromArgb(rgbColor.R, 0, rgbColor.B),
                Color.FromArgb(rgbColor.R, 255, rgbColor.B));

            colorSliderB.SetColors(
                Color.FromArgb(rgbColor.R, rgbColor.G, 0),
                Color.FromArgb(rgbColor.R, rgbColor.G, 255));

            //HSV sliders
            int h, s, v;
            Converter.RgbToHsv(rgbColor, out h, out s, out v);
            colorSliderS.SetColors(Converter.HsvToRgb(h, 0, v), Converter.HsvToRgb(h, 100, v));
            colorSliderV.SetColors(Converter.HsvToRgb(h, s, 0), Converter.HsvToRgb(h, s, 100));

            labelResult.BackColor = rgbColor;

        }

        private Color GetRgbColor()
        {
            return Color.FromArgb(
                colorSliderR.Value,
                colorSliderG.Value,
                colorSliderB.Value);
        }

        private void OnRGBValuesChanged(object sender, EventArgs e)
        {
            if (!_updating)
            {
                _updating = true;

                SetHsvValues();

                SetColorRanges();

                _updating = false;
            }
            
        }

        private void OnHsvValuesChanged(object sender, EventArgs e)
        {
            if (!_updating)
            {
                _updating = true;

                SetRgbValues();

                _updating = false;

                SetColorRanges();
            }
        }

        private void SetHsvValues()
        {
            Color rgbColor = GetRgbColor();
            int h, s, v;
            Converter.RgbToHsv(rgbColor, out h, out s, out v);

            colorSliderH.Value = h;
            colorSliderS.Value = s;
            colorSliderV.Value = v;
        }

        private void SetRgbValues()
        {
            int h = colorSliderH.Value;
            int s = colorSliderS.Value;
            int v = colorSliderV.Value;

            Color rgbColor = Converter.HsvToRgb(h, s, v);
            colorSliderR.Value = rgbColor.R;
            colorSliderG.Value = rgbColor.G;
            colorSliderB.Value = rgbColor.B;
        }


    }
}
