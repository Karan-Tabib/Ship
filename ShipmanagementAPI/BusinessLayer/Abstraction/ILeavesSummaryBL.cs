using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface ILeaveSummaryBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        LeaveSummaryDTO Get(int id);

        IEnumerable<LeaveSummaryDTO> GetAll(string crewId);


        // Method to add a new item
        LeaveSummaryDTO Add(LeaveSummaryDTO item);

        // Method to update an existing item
        LeaveSummaryDTO Update(LeaveSummaryDTO item);

        // Method to remove an item by its ID
        bool Remove(int id);
    }
}