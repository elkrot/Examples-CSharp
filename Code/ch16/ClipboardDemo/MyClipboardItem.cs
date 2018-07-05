using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClipboardDemo
{
    [Serializable]
    class MyClipboardItem
    {
        //we're naming it this, but we'll actually store a list of these
        public const string FormatName = "HowToCSharp.ch16.ClipboardDemo.MyClipboardItem";
        public static readonly DataFormats.Format Format;
        static MyClipboardItem()
        {
            Format = DataFormats.GetFormat(FormatName);
        }

        public string Name { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }

        public MyClipboardItem(string name, string sex, string age)
        {
            this.Name = name;
            this.Sex = sex;
            this.Age = age;
        }
    }
}
