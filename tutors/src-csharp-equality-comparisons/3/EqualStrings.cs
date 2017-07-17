using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.EqualityOp.EqualStrings
{
	class Program
	{
		static void Main(string[] args)
		{
			string str1 = "Click me now!";
			string str2 = string.Copy(str1);

			Console.WriteLine("Reference: " + ReferenceEquals(str1, str2));
			Console.WriteLine("Operator:  " + AreStringsEqualOp(str1, str2));
			Console.WriteLine("Method:    " + AreStringsEqualMethod(str1, str2));
		}


		static bool AreStringsEqualOp(string x, string y)
		{
			return x == y;
		}

		static bool AreStringsEqualMethod(string x, string y)
		{
			return x.Equals(y);
		}
	}



}
