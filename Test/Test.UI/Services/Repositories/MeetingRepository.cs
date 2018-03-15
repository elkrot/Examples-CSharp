using System.Threading.Tasks;
using System.Linq;
using Test.DataAccess;
using Test.Model;
using System.Data.Entity;
using System.Collections.Generic;

namespace Test.UI.Services.Repositories
{
    class MeetingRepository : IGenericRepository<Meeting, TestDbDataContext>, IMeetingRepository
    {
        public MeetingRepository(TestDbDataContext context) : base(context)
        {
        }
        public async override Task<Meeting> GetByIdAsync(int id)
        {
           return await  Context.Meetings.Include(m => m.Tests).SingleAsync(m => m.Id == id);
        }

        public async Task<List<TestEntity>> GetAllTestAsync() {
            return await Context.Set<TestEntity>()
                .ToListAsync();
        }
    }
}
