using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.Comparisons.CalorieCountCompare
{
	class Program
	{
		static void Main(string[] args)
		{
			CalorieCount cal300 = new CalorieCount(300);
			CalorieCount cal400 = new CalorieCount(400);

			DisplayOrder(cal300, cal400);
			DisplayOrder(cal400, cal300);
			DisplayOrder(cal300, cal300);

			if (cal300 < cal400)
				Console.WriteLine("cal300 < cal400");

			if (cal300 == cal400)
				Console.WriteLine("cal300 == cal400");			

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
