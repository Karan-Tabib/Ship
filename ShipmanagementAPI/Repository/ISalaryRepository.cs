using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface ISalaryRepository
    {  // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        SalaryInformation Get(int id);

        IEnumerable<SalaryInfoDTO> GetAll(int id);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<SalaryInformation> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        SalaryInformation Add(SalaryInformation item);

        // Method to update an existing item
        SalaryInformation Update(SalaryInformation item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}
