using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.ViewModels.OrderVms
{
    public class BaseOrderViewModel : BaseViewModel
    {
        private Order _order;
        public Order Order
        {
            get => _order;
            set
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ClientId
        {
            get => Order.ClientId;
            set
            {
                if (Order.ClientId != value)
                {
                    Order.ClientId = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime OrderDate
        {
            get => Order.OrderDate;
            set
            {
                if (Order.OrderDate != value)
                {
                    Order.OrderDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal? Total
        {
            get => Order.Total;
            set
            {
                if (Order.Total != value)
                {
                    Order.Total = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalNet));
                }
            }
        }
        public decimal? TotalNet
        {
            get => Order.TotalNet;
            set
            {
                if (Order.TotalNet != value)
                {
                    Order.TotalNet = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal? DiscountRate
        {
            get => Order.DiscountRate ?? 0;
            set
            {
                if (Order.DiscountRate != value)
                {
                    Order.DiscountRate = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalNet));
                }
            }
        }
        public Client Client
        {
            get => Order.Client;
            set
            {
                if (Order.Client != value)
                {
                    Order.Client = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<OrderItem> OrderItems
        {
            get => new ObservableCollection<OrderItem>(Order.OrderItems);
            set
            {
                if (Order.OrderItems != value)
                {
                    Order.OrderItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public BaseOrderViewModel()
        {
            _order = new Order();
        }
    }
}
