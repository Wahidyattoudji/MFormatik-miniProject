using MFormatik.Application.Services.Contracts;
using MFormatik.Core.Models;

namespace MFormatik.Application.MediatorService
{
    public class Mediator : IMediator
    {
        public IClientService ClientService { get; }
        public IProductService ProductService { get; }
        public IOrderService OrderService { get; }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                }
            }
        }

        private OrderItem _tempOrderItem;
        public OrderItem TempOrderItem
        {
            get { return _tempOrderItem; }
            set
            {
                if (_tempOrderItem == value) return;
                _tempOrderItem = value;
            }
        }


        public Mediator(IClientService clientService, IProductService productService, IOrderService orderService)
        {
            ClientService = clientService;
            ProductService = productService;
            OrderService = orderService;
        }

        private readonly Dictionary<string, List<Action<object>>> Subscribers = new();

        public void Subscribe(string message, Action<object> callback)
        {
            if (!Subscribers.ContainsKey(message))
            {
                Subscribers[message] = new List<Action<object>>();
            }
            Subscribers[message].Add(callback);
        }

        public void Unsubscribe(string message, Action<object> callback)
        {
            if (Subscribers.ContainsKey(message))
            {
                Subscribers[message].Remove(callback);
            }
        }

        public void Notify(string message, object param = null)
        {
            if (Subscribers.ContainsKey(message))
            {
                foreach (var callback in Subscribers[message])
                {
                    callback(param);
                }
            }
        }
    }

}
