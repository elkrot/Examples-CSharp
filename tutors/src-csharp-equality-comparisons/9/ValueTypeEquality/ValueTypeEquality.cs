using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.ValueTypes.ValueTypeEquality
{
	class Program
	{
		static void Main(string[] args)
		{
			FoodItem banana = new FoodItem("banana", FoodGroup.Fruit);
			FoodItem banana2 = new FoodItem("banana", FoodGroup.Fruit);
			FoodItem chocolate = new FoodItem("chocolate", FoodGroup.Sweets);

			Console.WriteLine(
				"banana    == banana2:   " + (banana == banana2));
			Console.WriteLine(
				"banana2   == chocolate: " + (banana2 == chocolate));
			Console.WriteLine(
				"chocolate == banana:    " + (chocolate == banana));

		}
	}
}
