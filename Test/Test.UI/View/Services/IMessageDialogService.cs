using System.Threading.Tasks;

namespace Test.UI.View.Services
{
    public interface IMessageDialogService
    {
        Task<MessageDialogResult> ShowOKCancelDialogAsync(string text, string title);
        Task ShowInfoDialogAsync(string text);
    }
}