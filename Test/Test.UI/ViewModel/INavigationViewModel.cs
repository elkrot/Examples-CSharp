using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.ViewModel
{
    public interface INavigationViewModel
    {
        Task LoadAsync();
    }
}