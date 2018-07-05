using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace CodeContracts
{
    class Program
    {
        [ContractClass(typeof(AddContract))]
        interface IAdd
        {
            UInt32 Add(UInt32 a, UInt32 b);
        }

        [ContractClassFor(typeof(IAdd))]
        class AddContract : IAdd
        {

            #region IAdd Members

            //private and explicit interface implementation
            UInt32 IAdd.Add(UInt32 a, UInt32 b)
            {
                Contract.Requires((UInt64)a + (UInt64)b < UInt32.MaxValue);
                return a+b;
            }

            #endregion
        }

        //this class does not need to specify the contracts
        class BetterAdd : IAdd
        {
            public UInt32 Add(UInt32 a, UInt32 b)
            {
                return a + b;
            }
        }

        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            AppendNumber(list, 13);

            AppendNumber(list, -1);

            BetterAdd ba = new BetterAdd();
            UInt32 result = ba.Add(UInt32.MaxValue, UInt32.MaxValue);

            Console.ReadKey();
        }

        static void AppendNumber(List<int> list, int newNumber)
        {
            Contract.Requires(newNumber > 0, "Failed contract: negative");
            Contract.Ensures(list.Count == Contract.OldValue(list.Count) + 1);

            list.Add(newNumber);
        }
    }
}
