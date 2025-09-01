using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface ICrewBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        CrewInfoDTO Get(int boatId);

        IEnumerable<CrewInfoDTO> GetAll(string userId);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();

        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        CrewInfoDTO Add(CrewInfoDTO item);

        // Method to update an existing item
        CrewInfoDTO Update(CrewInfoDTO item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}