using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality
{
	public enum FoodGroup { Meat, Fruit, Vegetables, Sweets }

	public struct FoodItem : IEquatable<FoodItem>
	{
		private readonly string _name;
		private readonly FoodGroup _group;

		public bool Equals(FoodItem other)
		{
			return this._name == other.Name && this._group == other._group;
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode() ^ _group.GetHashCode();
		}

		public static bool operator ==(FoodItem lhs, FoodItem rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(FoodItem lhs, FoodItem rhs)
		{
			return !lhs.Equals(rhs);
		}


		public override bool Equals(object obj)
		{
			if (obj is FoodItem)
				return Equals((FoodItem)obj);
			else
				return false;
		}



		public string Name { get { return _name; } }
		public FoodGroup Group { get { return _group; } }

		public FoodItem(string name, FoodGroup group)
		{
			this._name = name;
			this._group = group;
		}

		public override string ToString()
		{
			return _name;
		}


	}
}
