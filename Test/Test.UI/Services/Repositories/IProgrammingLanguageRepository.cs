using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.Services.Repositories
{
    public interface IProgrammingLanguageRepository:IGenericRepository<ProgrammingLanguage>
    {
        Task<bool> IsReferenceByTestAsync(int programmingLanguageId);
    }
}
