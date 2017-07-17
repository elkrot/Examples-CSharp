using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.Welcome.FloatArithmetic
{
	class Program
	{
		static void Main(string[] args)
		{
			float x = 5.05f;
			float y = 0.95f;

			Console.WriteLine(x + y);
			Console.WriteLine("x + y ==6? " + (x + y == 6.0f));

		}
	}
}
