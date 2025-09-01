using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface IShipRepository<T>
    {
        // Method to get an item by its ID
        Task<T> Get(Expression<Func<T,bool>> predicate);

        // Method to get all items
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> predicate);

        // Method to add a new item
        Task<T> Add(T item);

        // Method to update an existing item
        Task<T> Update(T item);

        // Method to remove an item by its ID
        Task<bool> Remove(T item);
    }
}
