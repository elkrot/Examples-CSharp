using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator.Data
{
    public interface IRepository
    {
        void Init();
        string GetRandomName();
        string GetRandomSurname();
        string GetRandomPatronymic();
        string GetRandomUniqueLogin();
        string GetRandomEmailDomain();
    }
}
