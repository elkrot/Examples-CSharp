using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Data;

namespace TestDataGenerator.UnitTests
{
    public class RepositoryMock : IRepository
    {
        public string GetRandomEmailDomain()
        {
            return "test.ru";
        }

        public string GetRandomName()
        {
            return "Иван";
        }

        public string GetRandomPatronymic()
        {
            return "Иванович";
        }

        public string GetRandomSurname()
        {
            return "Иванов";
        }

        public string GetRandomUniqueLogin()
        {
            return "iivan";
        }

        public void Init()
        {
           
        }
    }
}
