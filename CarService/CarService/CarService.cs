using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;

namespace CarService
{
 [ServiceBehavior(Namespace ="http://xxx.com")]
    public class CarService : ICarService
    {
        public CarInfo GetCar(CarRequest request)
        {

            if (request.Id == 0)
                throw new DivideByZeroException("не может быть 0");

            var file = ConfigurationManager.AppSettings["fileCar"];
            var doc = XDocument.Load(file);
            var element = doc.Descendants("Car").FirstOrDefault(x => x.Attribute("id").Value == request.Id.ToString());

            var type = element.Attribute("Type").Value;
            var result = new Car();
            switch (type)
            {
                case "Truck":
                    result = new TruckCar();
                    ((TruckCar)result).Capacity = double.Parse(
                        element.Element("Capacity").Value.ToString(CultureInfo.GetCultureInfo("en-US")));
                    break;
                case "Passenger":
                    result = new PassengerCar();
                    ((PassengerCar)result).Passengers = int.Parse(
                        element.Element("Passengers").Value.ToString(CultureInfo.GetCultureInfo("en-US")));
                    break;
                default:
                    result =new Car();
                    break;
            }


            result.Id = int.Parse(element.Attribute("id").Value);
            result.Vendor = element.Element("Vendor").Value;
            result.Model = element.Element("Model").Value;
            result.Year = int.Parse(element.Element("Year").Value);
            return new CarInfo(result);
        }

        public void SetCar(CarInfo car)
        {
            var file = ConfigurationManager.AppSettings["fileCar"];
            var doc = XDocument.Load(file);

            var element = new XElement("Car", new XAttribute("id", car.Id),
                new XElement("Vendor", car.Vendor),
                new XElement("Model", car.Model),
                new XElement("Year", car.Year)
                );



            if (car.Type==CarType.TruckCar)
            {
                element.Add(new XAttribute("Type", "TruckCar")
                    , new XElement("Capacity", (car).Capacity.ToString(CultureInfo.GetCultureInfo("en-US"))));
            }
            else 
            {
                element.Add(new XAttribute("Type", "Passenger"), new XElement("Passangers", (car).Passengers));
            }

            doc.Root.Add(element);
            doc.Save(file);
        }
    }
}
