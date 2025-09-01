using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface ICrewRepository
    {  // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        CrewInformation Get(int boatId);

        IEnumerable<CrewInformation> GetAll(int id);
        // Method to get all items
        
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<CrewInformation> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        CrewInformation Add(CrewInformation item);

        // Method to update an existing item
        CrewInformation Update(CrewInformation item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}
