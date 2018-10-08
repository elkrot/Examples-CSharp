using Client.CService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CService.ICarService client = new CService.CarServiceClient("BasicHttpBinding_ICarService");
            try
            {
                var car = client.GetCar(new CarRequest("zsasda-asdads-asd-czxqeq-asdadsa2asa-1ass", 1));
                Console.WriteLine(car.Passengers);
                car = client.GetCar(new CarRequest("zsasda-asdads-asd-czxqeq-asdadsa2asa-1ass", 0));
                Console.WriteLine(car.Passengers);

            }
            catch (FaultException e) {
                Console.WriteLine(e.Message);
            }
                          var   carz = client.GetCar(new CarRequest("zsasda-asdads-asd-czxqeq-asdadsa2asa-1ass", 1));
                Console.WriteLine(carz.Passengers);
            // Используйте переменную "client", чтобы вызвать операции из службы.
            Console.ReadKey();
            // Всегда закройте клиент.
            
        }
    }
}
