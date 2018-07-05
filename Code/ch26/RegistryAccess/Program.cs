using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace RegistryAccess
{
    class Program
    {
        [System.Security.Permissions.RegistryPermission(System.Security.Permissions.SecurityAction.Demand, ViewAndModify = "HKEY_CURRENT_USER")]
        static void Main(string[] args)
        {
            //read from HKLM
            using (RegistryKey hklm = Registry.LocalMachine)
            using (RegistryKey keyRun = hklm.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
            {
                foreach (string valueName in keyRun.GetValueNames())
                {
                    Console.WriteLine("Name: {0}\tValue: {1}", valueName, keyRun.GetValue(valueName));
                }
            }

            Console.WriteLine();

            //create our own registry key for the app
            //true indicates we want to be able to write to the subkey
            using (RegistryKey software = Registry.CurrentUser.OpenSubKey(@"Software", true))
            //volatile indicates that this key should be deleted when the computer restarts
            using (RegistryKey myKeyRoot = software.CreateSubKey("CSharp4HowTo",RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.Volatile))
            {
                //automatically determine the type
                myKeyRoot.SetValue("NumberOfChapters", 28);

                //specify the type
                myKeyRoot.SetValue("Awesomeness", Int64.MaxValue, RegistryValueKind.QWord);

                //display what we just created
                foreach (string valueName in myKeyRoot.GetValueNames())
                {
                    Console.WriteLine("{0}, {1}, {2}", valueName, myKeyRoot.GetValueKind(valueName), myKeyRoot.GetValue(valueName));
                }

                //remove from registry (set a breakpoint here to go look at it in regedit)
                software.DeleteSubKeyTree("CSharp4HowTo");
            }
        
            Console.WriteLine();
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();     
        }
    }
}
