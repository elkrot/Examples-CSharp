using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DynamicInstantiate
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom("DynamicInstantiateLib.dll");

            Type type = assembly.GetType("DynamicInstantiate.TestClass");
            object obj = Activator.CreateInstance(type);
            
            //invoke the Add method
            int result = (int)type.InvokeMember("Add", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, obj, new object[] { 1, 2 });
            Console.WriteLine("result: {0}", result);

            //invoke the Add method using dynamic
            dynamic testClass = Activator.CreateInstance(type);
            result = testClass.Add(5, 6);
            Console.WriteLine("result: {0}", result);

            //invoke the generic CombineStrings<T> method using methodInfo
            MethodInfo mi = type.GetMethod("CombineStrings");
            MethodInfo genericMi = mi.MakeGenericMethod(typeof(double));
            string combined = (string)genericMi.Invoke(obj, new object[]{2.5, 5.5});
            Console.WriteLine("combined: {0}", combined);

            //invoke the CombineStrings<T> method using dynamic
            combined = testClass.CombineStrings<double>(13.3, 14.4);
            Console.WriteLine("combined: {0}", combined);
            

            Console.ReadKey();
        }
    }
}
