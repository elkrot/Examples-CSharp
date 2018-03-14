using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Model;

namespace Test.UI.Services.Lookups
{
   public class LookupDataService : ILookupDataService
        , IProgrammingLanguageLookupDataService
        ,IMeetingLookupDataService
    {
        private Func<TestDbDataContext> _contextCreator;

        public LookupDataService(Func<TestDbDataContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetTestLookupAsync() {
            using (var context = _contextCreator()) {
                return await context.Tests.AsNoTracking()
                    .Select(f => new LookupItem { Id = f.TestKey, DisplayMember = f.TestTitle })
                    .ToListAsync() ;
            }
        }


        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync()
        {
            using (var context = _contextCreator())
            {
                return await context.ProgrammingLanguages.AsNoTracking()
                    .Select(f => new LookupItem { Id = f.Id, DisplayMember = f.Name })
                    .ToListAsync();
            }
        }

        public async Task<List<LookupItem>> GetMeetingLookupAsync()
        {
            using (var context = _contextCreator())
            {
                return await context.Meetings.AsNoTracking()
                    .Select(f => new LookupItem { Id = f.Id, DisplayMember = f.Title })
                    .ToListAsync();
            }
        }

    }
}
