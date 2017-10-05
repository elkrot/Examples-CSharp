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

        public ScriptGenerator(IRepository repository)
        {
            _repository = repository;
        }
        public string CreateScript(int EntityCount)
        {
            throw new NotImplementedException();
        }

        public UserEntity GenerateUser()
        {
            UserEntity entity = new UserEntity();

            entity.Login = _repository.GetRandomUniqueLogin();

            entity.Name = _repository.GetRandomName();
            entity.SurName = _repository.GetRandomSurname();
            entity.Patronymic = _repository.GetRandomPatronymic();

            string randomDomain = _repository.GetRandomEmailDomain();

            entity.Email = string.Format("{0}@{1}",entity.Login,randomDomain);
            return entity;

        }

        public string GetInsertLine()
        {
            throw new NotImplementedException();
        }

        public string GetValueLine(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
