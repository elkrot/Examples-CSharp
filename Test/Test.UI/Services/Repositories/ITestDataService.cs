using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.Services.Repositories
{
    public interface ITestRepository
    {
        Task<TestEntity> GetByIdAsync(int testKey);
        Task SaveAsync();
        bool HasChanges();
        void Add(TestEntity test);
        void Remove(TestEntity model);
    }
}