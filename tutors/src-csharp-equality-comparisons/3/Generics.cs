using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generics
{
	namespace Pluralsight.CSharpEquality.EqualityOp.Generics
	{
		class Program
		{
			static void Main(string[] args)
			{
				string str1 = "apple";
				string str2 = string.Copy(str1);
				DisplayWhetherArgsEqual(str1, str2);
			}

			static void DisplayWhetherArgsEqual<T>(T x, T y)
			{
				Console.WriteLine("Equal? " + object.Equals(x, y));

				// this line will not build - you can't in general use == with generic types
				// Console.WriteLine("Equal? " + (x == y));
			}
		}

	}
}
