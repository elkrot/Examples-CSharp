using System.Threading.Tasks;

namespace Test.UI.ViewModel
{
    public interface ITestDetailViewModel
    {
        Task LoadAsync(int? testId);
        bool HasChanges { get; }
    }
}