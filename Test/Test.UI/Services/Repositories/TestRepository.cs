using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Model;

namespace Test.UI.Services.Repositories
{
   public class TestRepository : GenericRepository<TestEntity, TestDbDataContext>,ITestRepository
    {
        public TestRepository(TestDbDataContext context) : base(context)
        {
        }

        public override async Task<TestEntity> GetByIdAsync(int testKey)
        {
            return await Context.Tests.Include(t => t.Questions).SingleAsync(t => t.TestKey == testKey);
        }

        public async Task<bool> HasMeetingAsync(int testId) {
            return await Context.Meetings.AsNoTracking()
                .Include(mbox => mbox.Tests)
                .AnyAsync(m => m.Tests.Any(t => t.TestKey == testId));
        }

        public void RemoveQuestion(Question model)
        {
            Context.Questions.Remove(model);
        }


    }
}
 // await Task.Delay(5000);