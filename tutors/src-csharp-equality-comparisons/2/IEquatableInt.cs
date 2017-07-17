using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.IEquatableInt
{
	class Program
	{
		static void Main(string[] args)
		{
			int three = 3;
			int threeAgain = 3;
			int four = 4;

			Console.WriteLine(three.Equals(threeAgain));
			Console.WriteLine(three.Equals(four));

		}
	}
}
