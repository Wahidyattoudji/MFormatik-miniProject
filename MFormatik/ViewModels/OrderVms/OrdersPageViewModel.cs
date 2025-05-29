using CommunityToolkit.Mvvm.Input;
using MFormatik.ViewModels.OrderVms;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace MFormatik.ViewModels
{
    public class OrdersPageViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand NavigateToAddOrderCommand { get; }
        public ICommand NavigateToOrdersListCommand { get; }


        private bool _isAddOrderChecked;
        public bool IsAddOrderChecked
        {
            get => _isAddOrderChecked;
            set
            {
                if (_isAddOrderChecked == value) return; // Avoid unnecessary updates
                _isAddOrderChecked = value;
                OnPropertyChanged();
            }
        }

        public OrdersPageViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            NavigateToAddOrderCommand = new RelayCommand(NavigateToAddOrder);
            NavigateToOrdersListCommand = new RelayCommand(NavigateToOrdersList);

            _isAddOrderChecked = true;
        }

        public void NavigateToAddOrder() => CurrentViewModel = _serviceProvider.GetRequiredService<AddOrderVM>();

        public void NavigateToOrdersList() => CurrentViewModel = _serviceProvider.GetRequiredService<OrdersListVM>();

        public void EnsureLoadOrders()
        {
            if (CurrentViewModel is OrdersListVM ordersListVM)
            {
                ordersListVM.EnsureDataLoadedAsync();
            }
        }

    }
}