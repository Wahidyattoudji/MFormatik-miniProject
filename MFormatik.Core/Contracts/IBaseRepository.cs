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
        Task<string> AddAsync(T entity);
        Task<string> EditAsync(T entity);
        Task<string> DeleteAsync(int id);
    }
}
