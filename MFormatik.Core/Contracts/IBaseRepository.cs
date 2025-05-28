using MFormatik.Core.DTOs;

namespace MFormatik.Core.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        //// Read
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> SearchAsync(string searchItem);
        //Task<T> FindByIdAsync(int id);
        //Task<IEnumerable<T>> FindByFunctionAsync(Expression<Func<T, bool>> predicate);

        // Write
        Task<Result> AddAsync(T entity);
        Task<Result> EditAsync(T entity);
        Task<Result> DeleteAsync(int id);
    }
}
