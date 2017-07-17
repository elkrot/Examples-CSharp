using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualStrings
{
	class Program
	{
		static void Main(string[] args)
		{
			string str1 = "Click me now!";
			string str2 = string.Copy(str1);

			Console.WriteLine("str1 == " + str1);
			Console.WriteLine("str2 == " + str2);
			Console.WriteLine(str1 == str2);
		}
	}
}
