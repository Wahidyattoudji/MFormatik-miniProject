using MFormatik.Application.Helpers;
using MFormatik.Core.Models;

namespace MFormatik.ViewModels.ProductVms
{
    public class ProductLineViewModel : BaseViewModel
    {
        public OrderItem OrderItem { get; }
        public IReadOnlyList<Product> Products { get; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
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
                }
            }
        }

        public string ProductName => SelectedProduct?.Name ?? "No Product";

        public decimal ProductUnitPrice
        {
            get => UnitPrice;
            set
            {
                UnitPrice = value;
                OnPropertyChanged();
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


        public int Quantity
        {
            get => OrderItem.Quantity;
            set
            {
                OrderItem.Quantity = value;
                OnPropertyChanged(nameof(UnitPrice));
                OnPropertyChanged(nameof(NetPrice));
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
                OnPropertyChanged(nameof(UnitPrice));
                OnPropertyChanged(nameof(NetPrice));
            }
        }
        public decimal? DiscountRate
        {
            get => OrderItem.DiscountRate;
            set
            {
                OrderItem.DiscountRate = value; OnPropertyChanged(nameof(DiscountRate));
                OnPropertyChanged(nameof(NetPrice));
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

        // 
        public decimal NetPrice
        {
            set
            {
                NetPrice = value;
                OnPropertyChanged(nameof(NetPrice));
            }
            get
            {
                return OrderCalculationHelper.CalculateNetAmount(UnitPrice, DiscountRate ?? 0);
                //var discount = (DiscountRate ?? 0) / 100m;
                //return Quantity * UnitPrice * (1 - discount);
            }
        }


        public ProductLineViewModel(OrderItem orderItem, IReadOnlyList<Product> products)
        {
            OrderItem = orderItem;
            Products = products;

            _selectedProduct = new();
            _selectedProduct = Products.FirstOrDefault(p => p.Id == OrderItem.ProductId);
        }

    }
}
