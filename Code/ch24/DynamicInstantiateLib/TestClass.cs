using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicInstantiate
{
    public class TestClass
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public string CombineStrings<T>(T a, T b)
        {
            return a.ToString() + ", " + b.ToString();
        }
    }
}
