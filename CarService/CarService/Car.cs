using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    [KnownType(typeof(TruckCar))]
    [KnownType(typeof(PassengerCar))]
    [DataContract]
    public class Car
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Vendor { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public int Year { get; set; }
    }
}
