using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality
{
	class Program
	{
		static void Main(string[] args)
		{
			var names = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
			names.Add("apple");
			names.Add("pear");
			names.Add("pineapple");
			names.Add("Apple");

			foreach (string name in names)
				Console.WriteLine(name);
		}
	}
}
