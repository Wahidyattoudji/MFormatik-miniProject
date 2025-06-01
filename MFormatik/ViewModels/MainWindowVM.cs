using CommunityToolkit.Mvvm.Input;
using MFormatik.Application.Helpers;
using MFormatik.Core.Models;
using MFormatik.Services.Abstracts;
using MFormatik.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
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
        public ICommand OpenWindowCommand { get; }

        private List<FavoriteView> _favorites;
        public List<FavoriteView> Favorites
        {
            get => _favorites;
            set
            {
                _favorites = value;
                OnPropertyChanged();
            }
        }


        public MainWindowVM(INavigationService navigationService, HomePage homePage, OrdersPage ordersPage)
        {
            _navigationService = navigationService;
            _homePage = homePage;
            _ordersPage = ordersPage;

            NavigateToHomeCommand = new RelayCommand(NavigateToHomePage);
            NavigateToOrdersCommand = new RelayCommand(NavigateToOrdersPage);
            OpenWindowCommand = new RelayCommand<string>(OpenFavWindow);
            LoadFavorites();
            IsHomeChecked = true;
            NavigateToHomePage();
            EventDispatcher.Subscribe("RefreshFavorites", RefreshFavorites);
        }


        private void OpenFavWindow(string viewName)
        {
            var viewType = Type.GetType($"MFormatik.Views.OrderViews.{viewName}");
            if (viewType == null)
            {
                MessageBox.Show($"Cant Find {viewName}");
                return;
            }
            var window = App.ServiceProvider.GetRequiredService(viewType) as Window;
            window?.Show();
        }

        private void LoadFavorites() => Favorites = FavoritesService.LoadFavorites();
        private void RefreshFavorites(object obj) => LoadFavorites();

        private void NavigateToHomePage() => _navigationService.NavigateTo(_homePage);
        private void NavigateToOrdersPage() => _navigationService.NavigateTo(_ordersPage);
    }
}
