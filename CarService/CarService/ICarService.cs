using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CarService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ICarService" в коде и файле конфигурации.
    [ServiceContract(Namespace ="http//:xxx.com")]
    public interface ICarService
    {
        [OperationContract]
        void SetCar(CarInfo car);
        [OperationContract]
        CarInfo GetCar(CarRequest id);
    }
}
