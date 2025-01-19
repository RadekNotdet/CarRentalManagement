using System.Linq.Expressions;

namespace CarRentalManagement.Server.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T,bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderyBy = null,
            List<string> includes = null);

        Task<T> GetAsync(Expression<Func<T,bool>> expression, List<string> includes = null);

        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(int id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
    }
}
