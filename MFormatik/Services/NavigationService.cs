using MFormatik.Services.Abstracts;
using System.Windows.Controls;

namespace MFormatik.Services
{
    public class NavigationService : INavigationService
    {
        private Frame _navigationFrame;

        public NavigationService(Frame navigationFrame)
        {
            _navigationFrame = navigationFrame;
        }

        public void SetNavigationFrame(Frame frame)
        {
            _navigationFrame = frame;
        }

        public void NavigateTo(Page newPage)
        {
            if (_navigationFrame.NavigationService != null &&
               _navigationFrame.Content != newPage)
            {
                _navigationFrame.NavigationService.Navigate(newPage);
            }
        }
    }
}
