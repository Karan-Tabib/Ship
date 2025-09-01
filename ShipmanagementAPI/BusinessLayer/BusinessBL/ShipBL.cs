using BusinessLayer.Abstraction;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class ShipBL<T> : IShipBL<T> where T : class
    {
        private readonly IShipRepository<T> _shipRepository;

        public ShipBL(IShipRepository<T> shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public async Task<T> Add(T item)
        {
            var data = await _shipRepository.Add(item);

            //_dbSet.Add(item);
            //await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _shipRepository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _shipRepository.GetAll(predicate);
            //return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _shipRepository.Get(predicate);
            //return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<bool> Remove(T record)
        {
            return await _shipRepository.Remove(record);
            //_dbSet.Remove(record);
            //await _dbContext.SaveChangesAsync();
            //return true;
        }

        public async Task<T> Update(T item)
        {
            await _shipRepository.Update(item); 
            //_dbSet.Update(item);
            //await _dbContext.SaveChangesAsync();
            return item;
        }

    }
}
