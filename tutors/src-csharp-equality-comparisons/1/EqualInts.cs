using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.Welcome.EqualInts
{
	class Program
	{
		static void Main(string[] args)
		{
			int three = 3;
			int threeAgain = 3;
			bool intCmp = (three == threeAgain);
			Console.WriteLine(string.Format("compare ints:       {0}", intCmp));

			bool objCmp = ((object)three == (object)threeAgain);
			Console.WriteLine(string.Format("compare objects     {0}", objCmp));

			bool itfCmp =
				((IComparable<int>)three == (IComparable<int>)threeAgain);
			Console.WriteLine(string.Format("compare interfaces: {0}", itfCmp));

		}
	}
}
