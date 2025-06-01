using CommunityToolkit.Mvvm.Input;
using MFormatik.Application.Helpers;
using MFormatik.Application.MediatorService;
using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using MFormatik.Helpers;
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
                _isAddProductVisible = true;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAddProductVisible));
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

        private bool _isAddProductVisible = false;
        public bool IsAddProductVisible
        {
            get => _isAddProductVisible;
            set
            {
                if (_isAddProductVisible == value) return;
                _isAddProductVisible = value;
                OnPropertyChanged();
            }
        }



        public AddOrderVM(IMediator mediator)
        {
            _mediator = mediator;

            _selectedClient = new();
            _selectedProduct = new();
            _clientsList = new ObservableCollection<ClientDTO>();
            _productList = new ObservableCollection<Product>();
            OrderItems = new ObservableCollection<OrderItem>();

            SaveCommand = new RelayCommand(() => SaveOrder());
            CancelCommand = new RelayCommand(CloseWindow);
            AddProductCommand = new RelayCommand(AddProductToOrder);

            EventDispatcher.Subscribe("RefreshValues", RefreshValues);
            EventDispatcher.Subscribe("DeleteProduct", DeleteProduct);
            EventDispatcher.Subscribe("ValidateProduct", ValidateProduct);

            LoadData();
            ClearData();
        }
        ~AddOrderVM()
        {
            _mediator.Unsubscribe("RefrshValues", RefreshValues);
        }

        private void ValidateProduct(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddProductToOrder()
        {
            var newOrderItem = new OrderItem { Quantity = 1 };
            var newLine = new ProductLineViewModel(newOrderItem, ProductsList) { Position = ProductLines.Count + 1 };
            ProductLines.Add(newLine);
        }

        private void DeleteProduct(object obj)
        {
            if (obj is ProductLineViewModel LineToDelete && ProductLines.Contains(LineToDelete))
            {
                ProductLines.Remove(LineToDelete);
                UpdatePositions();
                OnPropertyChanged(nameof(ProductLines));
            }
        }


        private void RefreshValues(object obj)
        {
            CalculateValues();
        }

        private void CalculateValues()
        {
            Total = OrderCalculationHelper.CalculateTotal(ProductLines.Select(pl => pl.UnitPrice));
            TotalNet = OrderCalculationHelper.CalculateTotalNet((decimal)Total, (decimal)DiscountRate!);
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

        public void UpdatePositions()
        {
            for (int i = 0; i < ProductLines.Count; i++)
                ProductLines[i].Position = i + 1;
        }

        private void CloseWindow()
        {
            // throw new NotImplementedException();
        }

        private async Task SaveOrder()
        {
            //var total = OrderCalculationHelper.CalculateTotal(ProductLines.Select(pl => pl.UnitPrice));
            //var totalNet = OrderCalculationHelper.CalculateTotalNet(total, (decimal)DiscountRate);

            var newOrder = new Order
            {
                ClientId = SelectedClient?.Id ?? 0,
                OrderDate = DateTime.Now,
                DiscountRate = DiscountRate ?? 0,
                Total = Total,
                TotalNet = TotalNet,
                OrderItems = ProductLines.Select(pl => new OrderItem
                {
                    ProductId = pl.SelectedProduct?.Id ?? 0,
                    Quantity = pl.Quantity,
                    UnitPrice = pl.UnitPrice,
                    DiscountRate = pl.DiscountRate,
                    Position = pl.Position
                }).ToList()
            };

            var result = await _mediator.OrderService.CreateOrderAsync(newOrder);
            MsgHelper.ShowInformation(result.Message, "Add Info");

        }
    }
}
