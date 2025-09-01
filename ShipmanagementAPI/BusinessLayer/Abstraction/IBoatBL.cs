using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface IBoatBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        BoatInfoDTO Get(int boatId);

        IEnumerable<BoatInfoDTO> GetAll(string userId);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();

        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        BoatInfoDTO Add(BoatInfoDTO item);

        // Method to update an existing item
        BoatInfoDTO Update(BoatInfoDTO item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}