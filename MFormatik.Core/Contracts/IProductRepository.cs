using MFormatik.Core.Models;

namespace MFormatik.Core.Contracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        //Task<IEnumerable<Product>> SearchProductsAsync(string searchItem);
        //Task<IEnumerable<Product>> FilterProductsAsync(Expression<Func<Product, bool>> predicate);
    }
}
