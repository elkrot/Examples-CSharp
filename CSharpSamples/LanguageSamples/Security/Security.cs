// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Runtime.InteropServices;

public class MainClass
{
    public static void Main() 
    {
        //Создать разрешение чтения FileIO.
        FileIOPermission FileIOReadPermission = new FileIOPermission(PermissionState.None);
        FileIOReadPermission.AllLocalFiles = FileIOPermissionAccess.Read;

        //Создать базовый набор разрешений
        PermissionSet BasePermissionSet = new PermissionSet(PermissionState.None); // PermissionState.Unrestricted с полным доверием.
        BasePermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

        PermissionSet grantset = BasePermissionSet.Copy();
        grantset.AddPermission(FileIOReadPermission);

        //Написать пример исходного файла для чтения.
        System.IO.File.WriteAllText("TEST.TXT", "File Content");

        //-------- Вызов метода с полным доверием -------- 
        try
        {
            Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
            ReadFileMethod();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //-------- Создать AppDomain с разрешением чтения FileIO. -------- 
        AppDomain sandbox = AppDomain.CreateDomain("Sandboxed AppDomain With FileIO.Read permission", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation, grantset, null);
        try
        {
            Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
            sandbox.DoCallBack(new CrossAppDomainDelegate(ReadFileMethod));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        

        //-------- Создать AppDomain без разрешения чтения FileIO. -------- 
        //Предположить, что произойдет ошибка безопасности.
        PermissionSet grantset2 = BasePermissionSet.Copy();
        AppDomain sandbox2 = AppDomain.CreateDomain("Sandboxed AppDomain Without FileIO.Read permission", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation, grantset2, null);
        try
        {
            Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
            sandbox2.DoCallBack(new CrossAppDomainDelegate(ReadFileMethod));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to end.");
        Console.ReadKey();
    }

    static public void ReadFileMethod()
    {
        string S = System.IO.File.ReadAllText("TEST.TXT");
        Console.WriteLine("File Content: " + S);
        Console.WriteLine("");
    }

}



    