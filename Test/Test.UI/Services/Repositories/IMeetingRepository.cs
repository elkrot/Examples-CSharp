using System.Collections.Generic;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Model;

namespace Test.UI.Services.Repositories
{
    public interface IMeetingRepository: IGenericRepository<Meeting>
    {
        Task<List<TestEntity>> GetAllTestAsync();
    }
}