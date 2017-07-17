using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality
{
	class FoodNameEqualityComparer : EqualityComparer<FoodItem>
	{
		private static readonly FoodNameEqualityComparer _instance 
			= new FoodNameEqualityComparer();
		public static FoodNameEqualityComparer Instance { get { return _instance; } }
		private FoodNameEqualityComparer() { }

		public override bool Equals(FoodItem x, FoodItem y)
		{
			return x.Name == y.Name;
		}

		public override int GetHashCode(FoodItem obj)
		{
			return obj.Name.GetHashCode();
		}
	}
}
