using System.Windows;

namespace Test.UI.View.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOKCancelDialog(string test, string title) {
            var result =MessageBox.Show(test, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK ? MessageDialogResult.OK :
                MessageDialogResult.Cancel;
        }

    public void ShowInfoDialog(string text) {
        MessageBox.Show(text, "Info");
    }

    }


    public enum MessageDialogResult {
        OK,Cancel
    }
}
