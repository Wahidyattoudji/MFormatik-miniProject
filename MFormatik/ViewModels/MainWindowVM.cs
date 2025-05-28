using CommunityToolkit.Mvvm.Input;
using MFormatik.Services.Abstracts;
using MFormatik.Views.Pages;
using System.Windows.Input;

namespace MFormatik.ViewModels
{
    public class MainWindowVM : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private readonly HomePage _homePage;
        private readonly OrdersPage _ordersPage;

        private bool _isHomeChecked;
        public bool IsHomeChecked
        {
            get => _isHomeChecked;
            set
            {
                _isHomeChecked = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToOrdersCommand { get; }


        public MainWindowVM(INavigationService navigationService, HomePage homePage, OrdersPage ordersPage)
        {
            _navigationService = navigationService;
            _homePage = homePage;
            _ordersPage = ordersPage;

            NavigateToHomeCommand = new RelayCommand(NavigateToHomePage);
            NavigateToOrdersCommand = new RelayCommand(NavigateToOrdersPage);

            IsHomeChecked = true;
            NavigateToHomePage();
        }

        private void NavigateToHomePage() => _navigationService.NavigateTo(_homePage);
        private void NavigateToOrdersPage() => _navigationService.NavigateTo(_ordersPage);
    }
}
