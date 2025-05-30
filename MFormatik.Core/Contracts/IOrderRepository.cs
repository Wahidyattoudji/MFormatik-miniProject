using MFormatik.Core.Const;
using MFormatik.Core.Models;
using System.Linq.Expressions;

namespace MFormatik.Core.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> SearchOrdersAsync(string searchItem);
        Task<IEnumerable<Order>> FilterOrdersAsync(Expression<Func<Order, bool>> predicate, string OrderBy = OrderDirection.Ascending);
    }
}
