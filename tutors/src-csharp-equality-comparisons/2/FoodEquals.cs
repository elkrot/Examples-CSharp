using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.FoodEquals
{
	class Program
	{
		static void Main(string[] args)
		{
			Food banana = new Food("banana");
			Food banana2 = new Food("banana");
			Food chocolate = new Food("chocolate");

			// behaviour for non-null
			Console.WriteLine(banana.Equals(chocolate));
			Console.WriteLine(banana.Equals(banana2));

			// behaviour for nulls
			Console.WriteLine(banana.Equals(null));
			Console.WriteLine(object.Equals(banana, null));
			Console.WriteLine(object.Equals(null, banana));
			Console.WriteLine(object.Equals(null, null));
		}


	}
	public class Food
	{
		private string _name;
		public string Name { get { return _name; } }

		public Food(string name)
		{
			this._name = name;
		}

		public override string ToString()
		{
			return _name;
		}

	}
}
