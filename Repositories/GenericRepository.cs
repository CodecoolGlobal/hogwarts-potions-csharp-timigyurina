using HogwartsPotions.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HogwartsPotions.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal HogwartsContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(HogwartsContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }



        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }

            //return await _dbSet.ToListAsync();

        }

        public virtual async Task<T?> GetAsync(int? id)
        {
            if (id is null)
                return null;

            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync(); // We can either do the savings here or do it in the Controller, calling the IUnitOfWork's Commit/CommitAsync method
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Update(entity); // Update does not have an async method
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            T? entityToDelete = await GetAsync(id);
            if (entityToDelete != null)
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete); // Remove does not have an async method

                await _context.SaveChangesAsync();
            }

        }

        public virtual async Task<bool> Exists(int id)
        {
            T? entity = await GetAsync(id);
            return entity != null;
        }

    }
}
