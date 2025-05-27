using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.Models;
using System.Data.Entity;
using VisaBOT.Core.Extentions;

namespace MFormatik.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IDbContextFactory<MFormatikContext> _contextFactory;
        private readonly MFormatikContext _context;

        public ProductRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<Product>();
            }
        }

    }
}
