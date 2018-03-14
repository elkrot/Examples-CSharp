using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.Services.Lookups
{
    public interface IMeetingLookupDataService
    {
        Task<List<LookupItem>> GetMeetingLookupAsync();
    }
}