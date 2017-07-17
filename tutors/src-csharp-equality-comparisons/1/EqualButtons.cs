using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pluralsight.CSharpEquality.EqualButtons
{
	class Program
	{
		static void Main(string[] args)
		{
			Button button1 = new Button();
			button1.Text = "Click me now!";

			Button button2 = new Button();
			button2.Text = "Click me now!";

			Console.WriteLine(button1 == button2);
		}
	}
}
