using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.Models;
using System.Data.Entity;
using System.Linq.Expressions;
using VisaBOT.Core.Extentions;

namespace MFormatik.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IDbContextFactory<MFormatikContext> _contextFactory;
        private readonly MFormatikContext _context;

        public OrderRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders.AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<Order>();
            }
        }

        public async Task<IEnumerable<Order>> FilterOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            try
            {
                return await _context.Orders.Where(predicate)
                                            .AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<Order>();
            }
        }

        public async Task<IEnumerable<Order>> SearchOrdersAsync(string searchItem)
        {
            try
            {
                return await _context.Orders.Where(x => x.Id.ToString() == searchItem
                                                              || x.OrderDate.ToString().Contains(searchItem)
                                                              || x.Client.FullName.Contains(searchItem)
                                                              || x.Client.Email.Contains(searchItem)
                                                              ).AsNoTracking()
                                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<Order>();
            }
        }
    }
}
