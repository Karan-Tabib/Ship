using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface ISalarySummaryBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        SalarySummaryDTO Get(int id);

        IEnumerable<SalarySummaryDTO> GetAll(string crewId);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();

        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        SalarySummaryDTO Add(SalarySummaryDTO item);

        // Method to update an existing item
        SalarySummaryDTO Update(SalarySummaryDTO item);

        // Method to remove an item by its ID
        bool Remove(int id);
    }
}