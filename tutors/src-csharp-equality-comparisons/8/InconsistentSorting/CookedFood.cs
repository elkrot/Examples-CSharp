using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality
{
	public sealed class CookedFood : Food, IEquatable<CookedFood>
	{
		#region equality implementation
		public bool Equals(CookedFood other)
		{
			if (!base.Equals(other))
				return false;
			return this._cookingMethod == other._cookingMethod;
		}

		public static bool operator ==(CookedFood x, CookedFood y)
		{
			return object.Equals(x, y);
		}

		public static bool operator !=(CookedFood x, CookedFood y)
		{
			return !object.Equals(x, y);
		}

		public override bool Equals(object obj)
		{
			//if (ReferenceEquals(obj, this))
			//				return true;
			if (!base.Equals(obj))
				return false;
			CookedFood rhs = (CookedFood)obj;
			return this._cookingMethod == rhs._cookingMethod;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this._cookingMethod.GetHashCode();
		}
		#endregion

		private string _cookingMethod;

		public string CookingMethod { get { return _cookingMethod; } }

		public CookedFood(string cookingMethod, string name, FoodGroup group)
			: base(name, group)
		{
			this._cookingMethod = cookingMethod;
		}

		public override string ToString()
		{
			return string.Format("{0} {1} ({2})", _cookingMethod, Name, Group);
		}
	}

}
