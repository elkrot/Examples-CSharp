using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LambdaExpressions
{
    public partial class Form1 : Form
    {
        Point prevPt = Point.Empty;
        public Form1()
        {
            InitializeComponent();

            this.MouseMove += (sender, e) =>
            {
                if (prevPt != e.Location)
                {
                    this.textBox1.AppendText(string.Format("MouseMove: ({0},{1})" + Environment.NewLine, e.X, e.Y));
                    prevPt = e.Location;
                }
            };

            //a single-line lambda expression is really simple
            this.MouseClick += (sender, e) => this.textBox1.AppendText(string.Format("MouseClick: ({0},{1}) {2}" + Environment.NewLine, e.X, e.Y, e.Button)); 
                        
            this.MouseDown += (sender,e) => this.textBox1.AppendText("MouseDown" + Environment.NewLine);
            this.MouseUp += (sender, e) => this.textBox1.AppendText("MouseUp" + Environment.NewLine);
        }
    }
}
