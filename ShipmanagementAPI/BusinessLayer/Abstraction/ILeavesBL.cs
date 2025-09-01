using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface ILeavesBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        LeaveInfoDTO Get(int id);

        IEnumerable<LeaveBoatDTO> GetAll(string crewId);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();

        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        LeaveInfoDTO Add(LeaveInfoDTO item);

        // Method to update an existing item
        LeaveInfoDTO Update(LeaveInfoDTO item);

        // Method to remove an item by its ID
        bool Remove(int id);
    }
}