using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.Models;

namespace MFormatik.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {

        }
    }
}
