using MFormatik.Core.Models;
using System.Linq.Expressions;

namespace MFormatik.Core.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> SearchOrdersAsync(string searchItem);

        Task<IEnumerable<Order>> FilterOrdersAsync<TKey>(
                         Expression<Func<Order, bool>>? predicate,
                         Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null,
                         Expression<Func<Order, TKey>> groupBy = null
            );
    }
}
