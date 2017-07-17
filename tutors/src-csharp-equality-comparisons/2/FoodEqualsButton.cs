using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pluralsight.CSharpEquality.FoodEqualsButton
{
	class Program
	{
		static void Main(string[] args)
		{
			Food food = new Food("chocolate");
			Button button = new Button();
			button.Text = "I'm a chocolate button - click me!";

			Console.WriteLine(food.Equals(button));

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
