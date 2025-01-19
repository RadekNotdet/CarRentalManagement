using CarRentalManagement.Server.Data;
using CarRentalManagement.Server.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentalManagement.Server.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete == null) 
            {
                return;
            }

            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();

        }

        public async void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null) 
            {
                foreach (var include in includes) 
                {
                    query = query.Include(include);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderyBy = null, 
            List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if(expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if(orderyBy != null)
            {
                query = orderyBy(query);
            }

            return await query.AsNoTracking().ToListAsync();

        }

        public async Task InsertAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            //_dbSet.Update(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
    }
}
