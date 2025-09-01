using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface ILeaveSummaryRepository
    {  // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        LeaveSummary Get(int boatId);

        IEnumerable<LeaveSummaryDTO> GetAll(int id);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<LeaveSummary> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        LeaveSummary Add(LeaveSummary item);

        // Method to update an existing item
        LeaveSummary Update(LeaveSummary item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}
