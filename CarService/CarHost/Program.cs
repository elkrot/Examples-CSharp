using CarService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host=new ServiceHost(typeof(CarService.CarService)))
            {
                host.Description.Behaviors.Add(new MainErrorHandlerBehavior());
                host.Open();

                Console.WriteLine("Host started...");
                Console.ReadLine();

            }
        }
    }
}
