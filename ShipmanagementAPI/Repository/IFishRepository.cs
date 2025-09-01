using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Linq.Expressions;

namespace Repository
{
    public interface IFishRepository
    {  // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        FishInformation Get(int boatId);

        IEnumerable<FishInformation> GetAll(int id);
        // Method to get all items
        //Task<IEnumerable<T>> GetAll();
        IEnumerable<FishInformation> GetAll();
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        FishInformation Add(FishInformation item);

        // Method to update an existing item
        FishInformation Update(FishInformation item);

        // Method to remove an item by its ID
        bool Remove(int item);

        IEnumerable<string> Search(string searchString);
    }
}
