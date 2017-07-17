using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.StringEquals
{
	class Program
	{
		static void Main(string[] args)
		{
			string banana = "banana";
			string banana2 = string.Copy(banana);

			Console.WriteLine(banana);
			Console.WriteLine(banana2);
			Console.WriteLine(ReferenceEquals(banana, banana2));
			Console.WriteLine(banana.Equals((object)banana2));
		}
	}
}
