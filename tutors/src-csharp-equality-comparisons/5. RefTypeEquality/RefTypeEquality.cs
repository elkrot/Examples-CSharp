using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.RefTypes.RefTypeEquality
{
	class Program
	{
		static void Main(string[] args)
		{
			Food apple = new Food("apple", FoodGroup.Fruit);
			CookedFood stewedApple = new CookedFood("stewed", "apple", FoodGroup.Fruit);
			CookedFood bakedApple = new CookedFood("baked", "apple", FoodGroup.Fruit);
			CookedFood stewedApple2 = new CookedFood("stewed", "apple", FoodGroup.Fruit);
			Food apple2 = new Food("apple", FoodGroup.Fruit);

			DisplayWhetherEqual(apple, stewedApple);
			DisplayWhetherEqual(stewedApple, bakedApple);
			DisplayWhetherEqual(stewedApple, stewedApple2);
			DisplayWhetherEqual(apple, apple2);
			DisplayWhetherEqual(apple, apple);

		}

		static void DisplayWhetherEqual(Food food1, Food food2)
		{
			if (food1 == food2)
				Console.WriteLine(string.Format("{0,12} == {1}", food1, food2));
			else
				Console.WriteLine(string.Format("{0,12} != {1}", food1, food2));
		}
	}
	
	
	public enum FoodGroup { Meat, Fruit, Vegetables, Sweets }

	public class Food
	{
		public static bool operator ==(Food x, Food y)
		{
			return object.Equals(x, y);
		}

		public static bool operator !=(Food x, Food y)
		{
			return !object.Equals(x, y);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals(obj, this))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			Food rhs = obj as Food;
			return this._name == rhs._name && this._group == rhs._group;
		}

		public override int GetHashCode()
		{
			return this._name.GetHashCode() ^ this._group.GetHashCode();
		}

		string _name;
		FoodGroup _group;

		public string Name { get { return _name; } }

		public Food(string name, FoodGroup group)
		{
			this._name = name;
			this._group = group;
		}

		public override string ToString()
		{
			return _name;
		}



	}
	
	
	
	
	
	
	public sealed class CookedFood : Food
	{
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
			if (!base.Equals(obj))
				return false;
			CookedFood rhs = (CookedFood)obj;
			return this._cookingMethod == rhs._cookingMethod;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this._cookingMethod.GetHashCode();
		}

		private string _cookingMethod;

		public string CookingMethod { get { return _cookingMethod; } }

		public CookedFood(string cookingMethod, string name, FoodGroup group)
			: base(name, group)
		{
			this._cookingMethod = cookingMethod;
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", _cookingMethod, Name);
		}



	}
	
}
