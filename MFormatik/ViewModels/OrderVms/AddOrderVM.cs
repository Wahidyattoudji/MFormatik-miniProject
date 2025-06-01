using CommunityToolkit.Mvvm.Input;
using MFormatik.Application.Helpers;
using MFormatik.Application.MediatorService;
using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using MFormatik.Helpers;
using MFormatik.Services.Abstracts;
using MFormatik.ViewModels.ProductVms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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
                _isAddProductVisible = Visibility.Visible;
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

        public ObservableCollection<ProductLineViewModel> FinalProductLines { get; set; } = new ObservableCollection<ProductLineViewModel>();
        public ObservableCollection<ProductLineViewModel> TempProductLines { get; set; } = new ObservableCollection<ProductLineViewModel>();

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand AddToFavoriteCommand { get; }
        public ICommand RemoveFavoriteCommand { get; }
        public ICloseable Closeable { get; set; }

        private bool _isInFavorite;
        public bool IsInFavorite
        {
            get => _isInFavorite;
            set
            {
                if (_isInFavorite == value) return;
                _isInFavorite = value;
            }
        }

        private bool _isSaveButtonEnble;
        public bool IsSaveButtonEnble
        {
            get => _isSaveButtonEnble;
            set
            {
                if (_isSaveButtonEnble == value) return;
                _isSaveButtonEnble = value;
                OnPropertyChanged();
            }
        }

        private Visibility _isAddProductVisible;
        public Visibility IsAddProductVisible
        {
            get => _isAddProductVisible;
            set
            {
                if (_isAddProductVisible == value) return;
                _isAddProductVisible = value;
                OnPropertyChanged();
            }
        }

        // Darag and Drop functionality
        public ICommand DropCommand { get; }
        public ProductLineViewModel DragData { get; set; }

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
            DropCommand = new RelayCommand<DropInfo>(OnDrop);

            AddToFavoriteCommand = new RelayCommand(AddTofavorite);
            RemoveFavoriteCommand = new RelayCommand(RemoveFromFavorite);

            IsSaveButtonEnble = false;

            EventDispatcher.Subscribe("RefreshValues", RefreshValues);
            EventDispatcher.Subscribe("DeleteProduct", DeleteProduct);
            EventDispatcher.Subscribe("ValidateProduct", ValidateProduct);
            PropertyChanged += ParentPropertyChanged;
            Chcekfavorite();
            LoadData();
            ClearData();
        }


        private void IsAllProductsAreValidated()
        {
            IsSaveButtonEnble = SelectedClient != null && (TempProductLines.Count == FinalProductLines.Count);
        }

        private async void SaveOrder()
        {
            CalculateValues();
            var newOrder = new Order
            {
                ClientId = SelectedClient?.Id ?? 0,
                OrderDate = DateTime.Now,
                DiscountRate = DiscountRate ?? 0,
                Total = Total,
                TotalNet = TotalNet,
                OrderItems = FinalProductLines.Select(pl => new OrderItem
                {
                    ProductId = pl.SelectedProduct?.Id ?? 0,
                    Quantity = pl.Quantity,
                    UnitPrice = pl.UnitPrice,
                    DiscountRate = pl.DiscountRate,
                    Position = pl.Position
                }).ToList()
            };
            var result = await _mediator.OrderService.CreateOrderAsync(newOrder);
            MsgHelper.ShowInformation("La commande a été validée", "Ajouter une information");
            ClearData();
            _mediator.Notify("ReloadOrdersList");
            CloseWindow();
        }

        private void OnDrop(DropInfo dropInfo)
        {
            var source = dropInfo.Data as ProductLineViewModel;
            var target = dropInfo.Target as ProductLineViewModel;

            if (source == null || target == null || source == target) return;

            int oldIndex = TempProductLines.IndexOf(source);
            int newIndex = TempProductLines.IndexOf(target);

            if (oldIndex >= 0 && newIndex >= 0)
            {
                TempProductLines.Move(oldIndex, newIndex);
            }
            UpdatePositions();
        }

        private void AddProductToOrder()
        {
            var newOrderItem = new OrderItem { Quantity = 1 };
            var newLine = new ProductLineViewModel(newOrderItem, ProductsList) { Position = TempProductLines.Count + 1 };
            TempProductLines.Add(newLine);
            IsAllProductsAreValidated();
        }

        private void ValidateProduct(object obj)
        {
            if (obj is ProductLineViewModel newLine && TempProductLines.Contains(newLine))
            {
                FinalProductLines.Add(newLine);
                CalculateValues();
                UpdatePositions();
                OnPropertyChanged(nameof(TempProductLines));
            }
        }

        private void DeleteProduct(object obj)
        {
            if (obj is ProductLineViewModel LineToDelete
                && (FinalProductLines.Contains(LineToDelete) || TempProductLines.Contains(LineToDelete))
                )
            {
                TempProductLines.Remove(LineToDelete);
                FinalProductLines.Remove(LineToDelete);
                CalculateValues();
                UpdatePositions();
                OnPropertyChanged(nameof(FinalProductLines));
            }
        }

        private void RefreshValues(object obj)
        {
            CalculateValues();
        }

        private async void LoadData()
        {
            await LoadClients();
            await LoadProducts();
            IsAddProductVisible = Visibility.Collapsed;
        }

        private async Task LoadProducts() => ProductsList = await _mediator.ProductService.GetAllProductsAsync();

        private async Task LoadClients() => ClientsList = await _mediator.ClientService.GetAllClientsASDtoAsync();

        public void UpdatePositions()
        {
            for (int i = 0; i < TempProductLines.Count; i++)
                TempProductLines[i].Position = i + 1;

            for (int i = 0; i < FinalProductLines.Count; i++)
                FinalProductLines[i].Position = i + 1;
        }

        private void ParentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DiscountRate))
            {
                CalculateValues();
            }
        }

        private void CalculateValues()
        {
            Total = OrderCalculationHelper.CalculateTotal(FinalProductLines.Select(pl => pl.NetPrice));
            TotalNet = OrderCalculationHelper.CalculateTotalNet((decimal)Total, (decimal)DiscountRate!);
            IsAllProductsAreValidated();
        }

        #region Favorites Manipulation
        private void Chcekfavorite()
        {
            var favoriteslist = FavoritesService.LoadFavorites();
            if (favoriteslist.Any(f => f.ViewName == "AddOrderView"))
            {
                IsInFavorite = true;
            }
        }

        private void AddTofavorite()
        {
            FavoritesService.AddFavorite(new FavoriteView
            {
                ViewName = "AddOrderView",
                ViewTitle = "Nouvelle Commande",
            });
            EventDispatcher.Notify("RefreshFavorites");
        }

        private void RemoveFromFavorite()
        {
            FavoritesService.RemoveFavorite("AddOrderView");
            EventDispatcher.Notify("RefreshFavorites");
        }
        #endregion
        private void ClearData()
        {
            SelectedClient = null;
            SelectedProduct = null;
            Total = 0;
            OrderDate = default;
            OrderItems.Clear();
            IsAddProductVisible = Visibility.Collapsed;
        }

        private void CloseWindow() => Closeable?.Close();
    }
}
