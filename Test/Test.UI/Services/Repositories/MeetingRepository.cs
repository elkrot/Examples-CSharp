using System.Threading.Tasks;
using System.Linq;
using Test.DataAccess;
using Test.Model;
using System.Data.Entity;
using System.Collections.Generic;
using System;

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

        public async Task ReloadTestAsync(int? testId)
        {
            var dbEntityEntry = Context.ChangeTracker.Entries<TestEntity>()
                .SingleOrDefault(db=>db.Entity.TestKey==testId);
            if (dbEntityEntry != null) {
                await dbEntityEntry.ReloadAsync();
            }
        }
    }
}
