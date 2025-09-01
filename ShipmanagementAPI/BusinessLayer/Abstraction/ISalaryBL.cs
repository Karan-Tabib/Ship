using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface ISalaryBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        SalaryInfoDTO Get(int id);

        IEnumerable<SalaryInfoDTO> GetAll(string crewId);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();

        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        SalaryInfoDTO Add(SalaryInfoDTO item);

        // Method to update an existing item
        SalaryInfoDTO Update(SalaryInfoDTO item);

        // Method to remove an item by its ID
        bool Remove(int id);
    }
}