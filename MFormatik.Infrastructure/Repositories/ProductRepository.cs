using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.Models;
using System.Linq.Expressions;

namespace MFormatik.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {

        }

        public Task<string> AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FilterProductsAsync(Expression expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchProductsAsync(string searchItem)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
