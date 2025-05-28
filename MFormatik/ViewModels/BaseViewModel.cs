using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFormatik.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region 
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

