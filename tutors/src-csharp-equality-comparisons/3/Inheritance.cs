using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.EqualityOp.Inheritance
{
	class Program
	{
		static void Main(string[] args)
		{
			object str1 = "apple";
			object str2 = string.Copy((string)str1);

			// The == operator will give the wrong result because it is not virtual
			// Equals() methods will work OK
			Console.WriteLine("Reference : " + ReferenceEquals(str1, str2));
			Console.WriteLine("Method    : " + str1.Equals(str2));
			Console.WriteLine("Operator  : " + (str1 == str2));
			Console.WriteLine("Static    : " + object.Equals(str1, str2));
		}
	}
}
