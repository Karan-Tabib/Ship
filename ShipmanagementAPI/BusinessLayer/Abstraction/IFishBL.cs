using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface IFishBL
    {
        // Method to get an item by its ID
        //Task<T> Get(Expression<Func<T, bool>> predicate);

        FishInfoDTO Get(int fishId);

        IEnumerable<FishInfoDTO> GetAll(string userId);
        // Method to get all items

        IEnumerable<FishInfoDTO> GetAll();

        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

        // Method to add a new item
        FishInfoDTO Add(FishInfoDTO item);

        // Method to update an existing item
        FishInfoDTO Update(FishInfoDTO item);

        IEnumerable<string> Search(string searchString);
        // Method to remove an item by its ID
        bool Remove(int item);
    }
}