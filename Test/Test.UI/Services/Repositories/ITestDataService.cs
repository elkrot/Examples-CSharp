using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.Services.Repositories
{


    public interface ITestRepository: IGenericRepository<TestEntity>
    {

        void RemoveQuestion(Question model);
        Task<bool> HasMeetingAsync(int testId);
    }
}