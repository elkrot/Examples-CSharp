using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Pluralsight.CSharpEquality.Strings.StringEquality
{
	class Program
	{
		static void Main(string[] args)
		{
			bool areEqual = string.Equals(
				"Apple", "Pineapple", StringComparison.CurrentCultureIgnoreCase);

			// To do an equals for which there is no Equals() method:
			int cmpResult = string.Compare(
				"Apple", "Pineapple", CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreSymbols);
			areEqual = (cmpResult == 0);

			// case-sensitive ordinal:
			areEqual = ("Apple" == "Pineapple");
			areEqual = "Apple".Equals("Pineapple");
		}
	}
}
