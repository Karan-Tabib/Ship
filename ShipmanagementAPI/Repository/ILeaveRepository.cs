using DataLayer.Entities;
using DTOs.DTO;

namespace Repository
{
    public interface ILeaveRepository
    {
        LeaveInformation Get(int id);

        IEnumerable<LeaveBoatDTO> GetAll(int id);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<LeaveInformation> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        LeaveInformation Add(LeaveInformation item);

        // Method to update an existing item
        LeaveInformation Update(LeaveInformation item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
    
}
