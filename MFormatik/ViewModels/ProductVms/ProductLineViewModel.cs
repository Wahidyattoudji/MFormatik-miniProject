using CommunityToolkit.Mvvm.Input;
using MFormatik.Application.Helpers;
using MFormatik.Core.Models;
using System.Windows;
using System.Windows.Input;

namespace MFormatik.ViewModels.ProductVms
{
    public class ProductLineViewModel : BaseViewModel
    {
        public OrderItem OrderItem { get; }
        public IReadOnlyList<Product> Products { get; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct ?? Products.First();
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OrderItem.ProductId = _selectedProduct?.Id ?? 0;
                    ProductUnitPrice = _selectedProduct.UnitPrice;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ProductName));
                    OnPropertyChanged(nameof(ProductUnitPrice));
                    EventDispatcher.Notify("RefreshValues");
                }
            }
        }
        // Product
        public string ProductName => SelectedProduct?.Name ?? "No Product";
        public decimal ProductUnitPrice
        {
            get => UnitPrice;
            set
            {
                UnitPrice = value;
                OnPropertyChanged();
                NotifyViewModel();
            }
        }
        public int ProductId
        {
            get => OrderItem.ProductId;
            set
            {
                OrderItem.ProductId = value;
                OnPropertyChanged(nameof(ProductId));
                OnPropertyChanged(nameof(ProductName));
            }
        }
        // OrderItem
        public int Quantity
        {
            get => OrderItem.Quantity;
            set
            {
                OrderItem.Quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NetPrice));
                NotifyViewModel();
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return OrderCalculationHelper.CalculateAmount(OrderItem.UnitPrice, Quantity);
            }
            set
            {
                OrderItem.UnitPrice = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NetPrice));
                NotifyViewModel();
            }
        }
        public decimal? DiscountRate
        {
            get => OrderItem.DiscountRate;
            set
            {
                OrderItem.DiscountRate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UnitPrice));
                OnPropertyChanged(nameof(NetPrice));
                NotifyViewModel();
            }
        }
        public int Position
        {
            get => OrderItem.Position;
            set
            {
                OrderItem.Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public decimal NetPrice
        {
            get => OrderCalculationHelper.CalculateNetAmount(UnitPrice, DiscountRate ?? 0);
            set
            {
                NetPrice = value;
                OnPropertyChanged(nameof(NetPrice));
            }
        }

        private Visibility _isValidateButtonVisible;
        public Visibility IsValidateButtonVisible
        {
            get => _isValidateButtonVisible;
            set
            {
                if (_isValidateButtonVisible == value) return;
                _isValidateButtonVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (_isEnable == value) return;
                _isEnable = value;
                OnPropertyChanged();
            }
        }


        public ICommand ValidateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public ProductLineViewModel(OrderItem orderItem, IReadOnlyList<Product> products)
        {
            OrderItem = orderItem;
            Products = products;

            ValidateProductCommand = new RelayCommand(ValidateProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);

            _selectedProduct = new();
            _selectedProduct = Products.FirstOrDefault(p => p.Id == OrderItem.ProductId);
            IsEnable = true;
        }

        private void ValidateProduct()
        {
            EventDispatcher.Notify("ValidateProduct", this);
            IsValidateButtonVisible = Visibility.Collapsed;
            IsEnable = false;
        }

        private void DeleteProduct() => EventDispatcher.Notify("DeleteProduct", this);
        private void NotifyViewModel() => EventDispatcher.Notify("RefreshValues");
    }
}
