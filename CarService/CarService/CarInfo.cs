using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace CarService
{
    [MessageContract(IsWrapped =true,WrapperName = "CarInfoWrapper" ,WrapperNamespace ="http://xxx.com")]
    public class CarInfo:IExtensibleDataObject
    {
        public CarInfo()
        {

        }

        public CarInfo(Car carInstance)
        {
            Id = carInstance.Id;
            Vendor = carInstance.Vendor;
            Model = carInstance.Model;
            Year = carInstance.Year;
            if (carInstance.GetType() == typeof(TruckCar))
            {
                Capacity = ((TruckCar)carInstance).Capacity;

                Type = CarType.TruckCar;
            }
            else {
                Passengers = ((PassengerCar)carInstance).Passengers;
                Type = CarType.PassengerCar;
            }
        }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public int Id { get; set; }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public string Vendor { get; set; }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public string Model { get; set; }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public int Year { get; set; }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public int Passengers { get; set; }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public double Capacity { get; set; }
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public CarType Type { get; set; }

        public ExtensionDataObject ExtensionData {
            get ;
            set ; }
    }
}
