using HolcimTC.Core.Interfaces;
using MFormatik.Application.Services.Contracts;
using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ObservableCollection<Order>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrdersAsync();
            return new ObservableCollection<Order>(orders);
        }


        public async Task<ObservableCollection<Order>> FilterByDateAsync(DateTime startDate, DateTime endDate, string OrderBy)
        {
            var OrdersByDate = await _unitOfWork.OrderRepository
                                                .FilterOrdersAsync(o => o.OrderDate >= startDate && o.OrderDate <= endDate, OrderBy);

            return new ObservableCollection<Order>(OrdersByDate ?? Enumerable.Empty<Order>());
        }

        public async Task<ObservableCollection<Order>> SearchOrdersAsync(string searchItem)
        {
            var orders = await _unitOfWork.OrderRepository.SearchOrdersAsync(searchItem);
            return new ObservableCollection<Order>(orders);
        }


        public async Task<Result> CreateOrderAsync(Order order)
        {
            return await _unitOfWork.OrderRepository.AddAsync(order);
        }
        public async Task<Result> UpdateOrderAsync(Order order)
        {
            return await _unitOfWork.OrderRepository.AddAsync(order);
        }
        public async Task<Result> DeleteOrderAsync(Order order)
        {
            return await _unitOfWork.OrderRepository.DeleteAsync(order.Id);
        }

    }

}
