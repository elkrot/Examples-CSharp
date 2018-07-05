using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CSharpToC
{
    class Program
    {
        [DllImport(@"..\..\..\Debug\MyCDll.dll",
            ExactSpelling=false, CallingConvention=CallingConvention.Cdecl,EntryPoint="SayHello")]
        public static extern int SayHello(
            [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, 
            int length);

        static void Main(string[] args)
        {
            int size = 32;
            byte[] buffer = new byte[size];
            int returnVal = SayHello(buffer, size);
            string result = Encoding.ASCII.GetString(buffer,0, returnVal);
            Console.WriteLine("\"{0}\", return value: {1}", result, returnVal);

            Console.ReadKey();

        }
    }
}
