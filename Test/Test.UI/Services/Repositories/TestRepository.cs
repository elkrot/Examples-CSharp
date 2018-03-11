using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Model;

namespace Test.UI.Services.Repositories
{
    public class TestRepository : ITestRepository
    {
        private TestDbDataContext _context;

        public TestRepository(TestDbDataContext context)
        {
            _context = context;
        }

        public void Add(TestEntity test)
        {
            _context.Tests.Add(test);
        }

        public async Task<TestEntity> GetByIdAsync(int testKey)
        {
           
            return await _context.Tests.Include(t=>t.Questions).SingleAsync(t => t.TestKey == testKey);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(TestEntity model)
        {
            _context.Tests.Remove(model);
        }

        public void RemoveQuestion(Question model)
        {
            _context.Questions.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
 // await Task.Delay(5000);