using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MFormatik.Application.Services.Contracts
{
    public interface IOrderService
    {
        Task<Result> CreateOrderAsync(Order order);
        Task<Result> UpdateOrderAsync(Order order);
        Task<Result> DeleteOrderAsync(Order order);

        Task<ObservableCollection<Order>> GetAllOrdersAsync();
        Task<ObservableCollection<Order>> SearchOrdersAsync(string searchItem);
        Task<ObservableCollection<Order>> FilterOrdersAsync(Expression<Func<Order, bool>> predicate);
    }
}
