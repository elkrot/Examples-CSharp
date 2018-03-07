namespace Test.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOKCancelDialog(string test, string title);
    }
}