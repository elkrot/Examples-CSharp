using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            #region isolation by user and assembly
            IsolatedStorageFile ifs = IsolatedStorageFile.GetUserStoreForAssembly();
             using (var t = new IsolatedStorageFileStream("data.txt", 
                 FileMode.Create, ifs))
             {
                 t.WriteByte(234);
             }
            #endregion

             #region isolation by machine and assembly
             IsolatedStorageFile ifs2 = IsolatedStorageFile.GetMachineStoreForAssembly();
             using (var t = new IsolatedStorageFileStream("data.txt", 
                 FileMode.Create, ifs2))
             {
                 t.WriteByte(234);
             }
             #endregion

        }
    }

