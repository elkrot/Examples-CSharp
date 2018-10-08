using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    [MessageContract(IsWrapped = true, WrapperName = "CarRequestWrapper", WrapperNamespace = "http://xxx.com")]
    public class CarRequest
    {
        [MessageBodyMember(Namespace = "http://xxx.com")]
        public int Id { get; set; }
        [MessageHeader(Namespace = "http://xxx.com")]
        public string LicenseKey { get; set; }
    }
}
