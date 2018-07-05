using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseConvert
{
    public partial class BaseConvertForm : Form
    {
        public bool calculating = false;

        public BaseConvertForm()
        {
            InitializeComponent();
        }

        private void OnSourceChanged(object sender, EventArgs e)
        {
            Recalculate();
        }

        private void OnBaseChanged(object sender, EventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {
            if (!calculating)
            {
                calculating = true;
                string source = textBoxSource.Text;
                int destBase = (int)numericUpDownBase.Value;
                Int64 sourceNum = 0;

                if (!string.IsNullOrEmpty(source) && Int64.TryParse(source, out sourceNum))
                {
                    switch (destBase)
                    {
                        case 2:
                        case 8:
                        case 10:
                        case 16:
                            textBoxDest.Text = Convert.ToString(sourceNum, destBase);
                            break;
                        default:
                            textBoxDest.Text = ConvertToBase(sourceNum, destBase);
                            break;
                    }
                }
                else
                {
                    textBoxDest.Text = "???";
                }
                calculating = false;
            }
        }

        private string ConvertToBase(Int64 decNum, int destBase)
        {
            StringBuilder sb = new StringBuilder();
            Int64 accum = decNum;
            while (accum > 0)
            {
                Int64 digit = (accum % destBase);
                string digitStr;
                if (digit <= 9)
                {
                    digitStr = digit.ToString();
                }
                else
                {
                    switch (digit)
                    {
                        case 10: digitStr = "A"; break;
                        case 11: digitStr = "B"; break;
                        case 12: digitStr = "C"; break;
                        case 13: digitStr = "D"; break;
                        case 14: digitStr = "E"; break;
                        case 15: digitStr = "F"; break;
                        //don't know what do after base-16
                        default: digitStr = "?"; break;
                    }
                }
                sb.Append(digitStr);
                accum /= destBase;
            }
            return sb.ToString();
        }

        private Int64 ConvertFromBase(string num, int fromBase)
        {
            Int64 accum = 0;
            Int64 multiplier = 1;
            for (int i = num.Length - 1; i >= 0; i--)
            {
                int digitVal;
                if (num[i] >= '0' && num[i] <= '9')
                {
                    digitVal = num[i] - '0';
                }
                else
                {
                    switch (num[i])
                    {
                        case 'A': digitVal = 10; break;
                        case 'B': digitVal = 11; break;
                        case 'C': digitVal = 12; break;
                        case 'D': digitVal = 13; break;
                        case 'E': digitVal = 14; break;
                        case 'F': digitVal = 15; break;
                        default: throw new FormatException("Unknown digit");
                    }
                }
                accum += (digitVal * multiplier);
                multiplier *= fromBase;
            }
            return accum;
        }

        private void OnDestChanged(object sender, EventArgs e)
        {
            if (!calculating)
            {
                calculating = true;
                string num = textBoxDest.Text;
                if (!string.IsNullOrEmpty(num))
                {
                    textBoxSource.Text = ConvertFromBase(num, (int)numericUpDownBase.Value).ToString();
                }
                calculating = false;
            }
        }
    }
}
