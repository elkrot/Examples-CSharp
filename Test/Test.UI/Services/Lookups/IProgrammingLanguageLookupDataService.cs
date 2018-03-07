using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.Services.Lookups
{
    public interface IProgrammingLanguageLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync();
    }
}