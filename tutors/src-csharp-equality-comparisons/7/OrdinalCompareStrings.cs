using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.OrdinalCompareStrings
{
	class Program
	{
		static void Main(string[] args)
		{
			int result = string.Compare(
				"lemon", "LEMON", StringComparison.Ordinal);
			Console.WriteLine("Compare result is " + result);
		}
	}
}
