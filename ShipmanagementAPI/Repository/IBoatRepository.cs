using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface IBoatRepository
    {  // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        BoatInformation Get(int boatId);

        IEnumerable<BoatInformation> GetAll(int id);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<BoatInformation> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        BoatInformation Add(BoatInformation item);

        // Method to update an existing item
        BoatInformation Update(BoatInformation item);

        // Method to remove an item by its ID
        bool Remove(int item);
    }
}
