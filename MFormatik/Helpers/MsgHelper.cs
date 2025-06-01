using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace MFormatik.Helpers
{
    public static class MsgHelper
    {
        public static void ShowInformation(string msg, string title) =>
                MessageBox.Show($"{msg}", $"{title}", MessageBoxButton.OK, MessageBoxImage.Information);

        public static void ShowError(string msg, string title) =>
                MessageBox.Show($"{msg}", $"{title}", MessageBoxButton.OK, MessageBoxImage.Error);

        public static void ShowServerError() =>
            MessageBox.Show("Please Make Sure that The Connection Is ON"
                , "No Connection To Server"
                , MessageBoxButton.OK
                , MessageBoxImage.Information);

        public static void ShowNoSelectionError(string Item) =>
            MessageBox.Show($"You Must Chosse a {Item} First", $"No Selected {Item}", MessageBoxButton.OK, MessageBoxImage.Information);

        public static bool DeleteConfirmation(string DeletingitemName)
        {
            string message = $"Are you sure you want to delete this {DeletingitemName}? This action cannot be undone.";
            var result = MessageBox.Show(
                message,
                "Delete Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }

        public static bool ExitConfirmation(string ExitMsg)
        {
            var result = MessageBox.Show(
                ExitMsg,
                "Confirm Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public static void ShowException(Exception ex) =>
            MessageBox.Show($" {ex.Message} ", $"Error", MessageBoxButton.OK, MessageBoxImage.Error);

        public static void ShowExceptionMsg(string msg) =>
            MessageBox.Show($" {msg} ", $"Error", MessageBoxButton.OK, MessageBoxImage.Error);

    }
}
