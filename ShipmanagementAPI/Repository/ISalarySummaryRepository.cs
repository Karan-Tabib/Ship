using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface ISalarySummaryRepository
    {  // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        SalarySummary Get(int boatId);

        IEnumerable<SalarySummaryDTO> GetAll(int id);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<SalarySummary> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        SalarySummary Add(SalarySummary item);

        // Method to update an existing item
        SalarySummary Update(SalarySummary item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}
