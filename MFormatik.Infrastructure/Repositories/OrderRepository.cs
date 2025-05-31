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
                return await _context.Orders.Include(x => x.Client)
                                            .Include(x => x.OrderItems)
                                            .AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<Order>();
            }
        }


        public async Task<IEnumerable<Order>> FilterOrdersAsync<TKey>(
                            Expression<Func<Order, bool>>? predicate,
                            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null,
                            Expression<Func<Order, TKey>> groupBy = null)
        {
            IQueryable<Order> query = _context.Orders.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (groupBy != null)
            {
                return await query
                    .GroupBy(groupBy)
                    .SelectMany(g => g)
                    .Distinct()
                    .ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Order>> SearchOrdersAsync(string searchItem)
        {
            try
            {
                return await _context.Orders.Include(x => x.Client)
                                             .Where(x => x.Id.ToString() == searchItem
                                                              || x.OrderDate.ToString().Contains(searchItem)
                                                              || x.TotalNet.ToString().Contains(searchItem)
                                                              || x.Total.ToString().Contains(searchItem)
                                                              || x.Client.FirstName.Contains(searchItem)
                                                              || x.Client.LastName.Contains(searchItem)
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
