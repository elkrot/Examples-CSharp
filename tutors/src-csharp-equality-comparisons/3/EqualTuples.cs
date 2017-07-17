using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralsight.CSharpEquality.EqualityOp.EqualTuples
{
	class Program
	{
		static void Main(string[] args)
		{
			Tuple<int, int> tuple1 = Tuple.Create(1, 3); 
			Tuple<int, int> tuple2 = Tuple.Create(1, 3);

			Console.WriteLine("Reference: " + 
				ReferenceEquals(tuple1, tuple2));
			Console.WriteLine("Method:    " + tuple1.Equals(tuple2));
			Console.WriteLine("Operator:  " + (tuple1 == tuple2));
		}
	}
}
