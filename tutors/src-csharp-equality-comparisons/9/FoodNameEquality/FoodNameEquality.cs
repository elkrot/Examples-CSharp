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
			FoodItem beetroot = new FoodItem("beetroot", FoodGroup.Vegetables);
			FoodItem pickledBeetroot = new FoodItem("beetroot", FoodGroup.Sweets);

			var eqComparer = FoodNameEqualityComparer.Instance;
			bool equals = eqComparer.Equals(beetroot, pickledBeetroot);

			Console.WriteLine("Equals? " + equals.ToString());
			Console.WriteLine(eqComparer.GetHashCode(beetroot));
			Console.WriteLine(eqComparer.GetHashCode(pickledBeetroot));

		}
	}
}
