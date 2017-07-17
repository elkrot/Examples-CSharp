using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.EqualityOp.EqualValueTypes
{
	class Program
	{
		static void Main(string[] args)
		{
			Food banana = new Food("banana");
			Food banana2 = new Food("banana");
			Food chocolate = new Food("chocolate");

			Console.WriteLine(banana.Equals(chocolate));
			Console.WriteLine(banana.Equals(banana2));

			// this line won't compile because no == overload has been provide for Food,
			// and Food is a valuetype
			Console.WriteLine(banana == chocolate);
		}
	}
	
		public struct Food
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
