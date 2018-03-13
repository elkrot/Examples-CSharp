using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Model;

namespace Test.UI.Services.Repositories
{
   public class TestRepository : IGenericRepository<TestEntity, TestDbDataContext>
    {
        public TestRepository(TestDbDataContext context) : base(context)
        {
        }

        public override async Task<TestEntity> GetByIdAsync(int testKey)
        {
            return await Context.Tests.Include(t => t.Questions).SingleAsync(t => t.TestKey == testKey);
        }


        public void RemoveQuestion(Question model)
        {
            Context.Questions.Remove(model);
        }


    }
}
 // await Task.Delay(5000);