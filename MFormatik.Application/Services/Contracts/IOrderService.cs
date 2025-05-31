using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.Application.Services.Contracts
{
    public interface IOrderService
    {
        Task<Result> CreateOrderAsync(Order order);
        Task<Result> UpdateOrderAsync(Order order);
        Task<Result> DeleteOrderAsync(Order order);

        Task<ObservableCollection<Order>> GetAllOrdersAsync();
        Task<ObservableCollection<Order>> SearchOrdersAsync(string searchItem);

        Task<ObservableCollection<Order>> FilterByDateAsync(DateTime startDate, DateTime endDate
                                                             , string? orderDirection = null);

        Task<ObservableCollection<Order>> GroupByOrderDateAsync();
        Task<ObservableCollection<Order>> GroupByClientAsync();
        Task<ObservableCollection<Order>> GroupByTotalNetAsync();

    }
}
