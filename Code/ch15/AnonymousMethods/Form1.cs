using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnonymousMethods
{
    public partial class Form1 : Form
    {
        Point prevPt = Point.Empty;
        public Form1()
        {
            InitializeComponent();

            this.MouseMove += delegate(object sender, MouseEventArgs e)
                {
                    if (prevPt != e.Location)
                    {
                        this.textBox1.AppendText(string.Format("MouseMove: ({0},{1})" + Environment.NewLine, e.X, e.Y));
                        prevPt = e.Location;
                    }
                };

            this.MouseClick += delegate(object sender, MouseEventArgs e)
            { this.textBox1.AppendText(string.Format("MouseClick: ({0},{1}) {2}" + Environment.NewLine, e.X, e.Y, e.Button)); };
            
            //don't need the method args? don't include them
            this.MouseDown += delegate {this.textBox1.AppendText("MouseDown"+Environment.NewLine);};
            this.MouseUp += delegate { this.textBox1.AppendText("MouseUp" + Environment.NewLine); };
        }
    }
}
