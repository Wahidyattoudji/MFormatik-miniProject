using System.Windows.Controls;

namespace MFormatik.Services.Abstracts
{
    public interface INavigationService
    {
        void SetNavigationFrame(Frame frame);
        void NavigateTo(Page newPage);
    }
}
