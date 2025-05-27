using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.Application.Services.Contracts
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);

        Task<ObservableCollection<Order>> GetAllOrdersAsync();
        Task<ObservableCollection<Order>> SearchOrdersAsync(string searchItem);
        Task<ObservableCollection<Order>> FilterOrdersAsync(Func<Order, bool> predicate);
        Task<ObservableCollection<Order>> GetOrdersByClientIdAsync(int clientId);
    }
}
