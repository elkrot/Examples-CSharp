using System;
using System.Management;
using System.Windows.Forms;

namespace HardwareInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*There is a wealth of hardware information available in
             the WMI hardware classes.
             See http://msdn.microsoft.com/en-us/library/aa389273.aspx 
             */
            Console.WriteLine("Machine: {0}", Environment.MachineName);
            Console.WriteLine("# of processors (logical): {0}", Environment.ProcessorCount);
            Console.WriteLine("# of processors (physical): {0}", CountPhysicalProcessors());
            Console.WriteLine("RAM installed: {0:N0} bytes", CountPhysicalMemory());
            Console.WriteLine("Is OS 64-bit? {0}", Environment.Is64BitOperatingSystem);
            Console.WriteLine("Is process 64-bit? {0}", Environment.Is64BitProcess);
            Console.WriteLine("Little-endian: {0}", BitConverter.IsLittleEndian);

            foreach (Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                Console.WriteLine("Screen {0}", screen.DeviceName);
                Console.WriteLine("\tPrimary {0}", screen.Primary);
                Console.WriteLine("\tBounds: {0}", screen.Bounds);
                Console.WriteLine("\tWorking Area: {0}", screen.WorkingArea);
                Console.WriteLine("\tBitsPerPixel: {0}", screen.BitsPerPixel);
            }
            
            Console.ReadKey();
        }

        private static UInt32 CountPhysicalProcessors()
        {
            //you must add a reference to the System.Management assembly
            ManagementObjectSearcher objects = 
                new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectCollection coll = objects.Get();
            foreach(ManagementObject obj in coll)
            {
                return (UInt32)obj["NumberOfProcessors"];
            }
            return 0;
        }

        private static UInt64 CountPhysicalMemory()
        {
            ManagementObjectSearcher objects = 
                new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectCollection coll = objects.Get();
            UInt64 total = 0;
            foreach (ManagementObject obj in coll)
            {
                total += (UInt64)obj["Capacity"];
            }
            return total;
        }
    }
}
