using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringIntCompare
{
	class Program
	{
		static void Main(string[] args)
		{
			string apple = "apple";
			string pear = "pear";

			DisplayOrder(apple, pear);
			DisplayOrder(pear, apple);
			DisplayOrder(apple, apple);

			DisplayOrder(3, 4);
			DisplayOrder(4, 3);
			DisplayOrder(3, 3);
		}

		static void DisplayOrder<T>(T x, T y) where T : IComparable<T>
		{
			int result = x.CompareTo(y);
			if (result == 0)
				Console.WriteLine("{0,12} = {1}", x, y);
			else if (result > 0)
				Console.WriteLine("{0,12} > {1}", x, y);
			else
				Console.WriteLine("{0,12} < {1}", x, y);
		}
	}
}
