
using DataLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class ShipRepository<T> :IShipRepository<T> where T:class
    {
        private readonly ShipManagemntDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public ShipRepository(ShipManagemntDbContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Add(T item)
        {
            _dbSet.Add(item);
           await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            var val = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            return val;
        }

        public async Task<bool> Remove(T record)
        {
            _dbSet.Remove(record);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> Update(T item)
        {
           _dbSet.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

    }
}
