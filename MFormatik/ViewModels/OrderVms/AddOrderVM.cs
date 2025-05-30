using CommunityToolkit.Mvvm.Input;
using MFormatik.Application.MediatorService;
using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using MFormatik.ViewModels.ProductVms;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MFormatik.ViewModels.OrderVms
{
    public class AddOrderVM : BaseOrderViewModel
    {
        private readonly IMediator _mediator;

        private ClientDTO _selectedClient;
        private Product _selectedProduct;
        private ObservableCollection<ClientDTO> _clientsList;
        private ObservableCollection<Product> _productList;


        public ClientDTO SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                if (_selectedClient == value) return;
                _selectedClient = value;
                OnPropertyChanged();
            }
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct == value) return;
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ClientDTO> ClientsList
        {
            get { return _clientsList; }
            set
            {
                if (_clientsList == value) return;
                _clientsList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Product> ProductsList
        {
            get { return _productList; }
            set
            {
                if (_productList == value) return;
                _productList = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<ProductLineViewModel> ProductLines { get; set; } = new ObservableCollection<ProductLineViewModel>();


        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddProductCommand { get; }


        public AddOrderVM(IMediator mediator)
        {
            _mediator = mediator;

            _selectedClient = new();
            _selectedProduct = new();
            _clientsList = new ObservableCollection<ClientDTO>();
            _productList = new ObservableCollection<Product>();
            OrderItems = new ObservableCollection<OrderItem>();

            SaveCommand = new RelayCommand(SaveOrder);
            CancelCommand = new RelayCommand(CloseWindow);
            AddProductCommand = new RelayCommand(AddProductToOrder);

            LoadData();
            ClearData();
        }

        private async void LoadData()
        {
            await LoadClients();
            await LoadProducts();
        }

        private async Task LoadProducts() => ProductsList = await _mediator.ProductService.GetAllProductsAsync();

        private async Task LoadClients() => ClientsList = await _mediator.ClientService.GetAllClientsASDtoAsync();

        private void ClearData()
        {
            SelectedClient = null;
            SelectedProduct = null;
            Total = 0;
            OrderDate = default;
            OrderItems.Clear();
        }

        private void AddProductToOrder()
        {
            var newOrderItem = new OrderItem { Quantity = 1 };
            var newLine = new ProductLineViewModel(newOrderItem, ProductsList) { Position = ProductLines.Count + 1 };
            ProductLines.Add(newLine);
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < ProductLines.Count; i++)
                ProductLines[i].Position = i + 1;
        }

        private void CloseWindow()
        {
            // throw new NotImplementedException();
        }

        private void SaveOrder()
        {
            // throw new NotImplementedException();
        }
    }
}
