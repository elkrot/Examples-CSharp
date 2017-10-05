using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Data;

namespace TestDataGenerator.BL
{
    public class ScriptGenerator : IScriptGenerator
    {
        private readonly IRepository _repository;
        private Random _random = new Random();
        public ScriptGenerator(IRepository repository)
        {
            _repository = repository;
        }
        public string CreateScript(int EntityCount)
        {
            IEnumerable<UserEntity> users = Enumerable.Repeat(GenerateUser(), EntityCount);
            IEnumerable<string> valueLines = users.Select(GetValueLine);
            string insertLine = GetInsertLine();
            string result = MergeLines(valueLines, insertLine);
            return result;
        }
        public string MergeLines(IEnumerable<string> valueLines, string insertLine) {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(insertLine);
            int i = 0;
            foreach (var item in valueLines)
            {
                if (i > 0) builder.Append(",");
                builder.Append(item);
                i++;
            }
            return builder.ToString();
        }
        public UserEntity GenerateUser()
        {
            UserEntity entity = new UserEntity();

            entity.Login = _repository.GetRandomUniqueLogin();

            entity.Name = _repository.GetRandomName();
            entity.SurName = _repository.GetRandomSurname();
            entity.Patronymic = _repository.GetRandomPatronymic();
            entity.Password = _random.Next(1000, 10000).ToString(); 
            string randomDomain = _repository.GetRandomEmailDomain();
            int year = _random.Next(2010, 2017);
            int month = _random.Next(1, 13);
            int day = _random.Next(1, 29);
if (year == 2016 && month > 2) month = 2;
            entity.RegistrationDate = new DateTime(year, month, day);
            
                entity.Email = string.Format("{0}@{1}",entity.Login,randomDomain);
            return entity;

        }

        public string GetInsertLine()
        {
            return @"INSERT INTO BlogUser (Name,Surname,Patronymic,Email,Login,Password,RegistrationDate)"; 
        }

        public string GetValueLine(UserEntity entity)
        {
            string registrationDate = entity.RegistrationDate.ToString("yyyyMMdd");
            string result =$"VALUES({entity.Name},{entity.SurName},{entity.Patronymic},{entity.Email},{entity.Login},{entity.Password},{registrationDate})";
            return result;
        }
    }
}
