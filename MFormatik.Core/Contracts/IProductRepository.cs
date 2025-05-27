using MFormatik.Core.Models;
using System.Linq.Expressions;

namespace MFormatik.Core.Contracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> SearchProductsAsync(string searchItem);
        Task<IEnumerable<Product>> FilterProductsAsync(Expression<Func<Product, bool>> predicate);

        Task<string> AddProductAsync(Product product);
        Task<string> UpdateProductAsync(Product product);
        Task<string> DeleteProductAsync(int id);
    }
}
