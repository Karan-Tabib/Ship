
using DataLayer.Entities;
using DataLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Repository.Repositories
{
    public class FishRepository : IFishRepository
    {
        private readonly ShipManagemntDbContext _context;

        public FishRepository(ShipManagemntDbContext context)
        {
            _context = context;
        }

        public FishInformation Add(FishInformation item)
        {
           _context.FishInformation.Add(item);
            _context.SaveChanges();
            return item;
        }

        public FishInformation Get(int fishId)
        {
            FishInformation? fishInformation = null;
             fishInformation = _context.FishInformation.FirstOrDefault(rec =>rec.FishId == fishId);
            return fishInformation;
        }
        public IEnumerable<FishInformation> GetAll()
        {
            return _context.FishInformation.ToList();
        }
        public bool Remove(int item)
        {
            var recordtodelete = Get(item);
            if (recordtodelete == null)
            {
                return false;
            }
            _context.FishInformation.Remove(recordtodelete);
            _context.SaveChanges();
            return true;
        }

        public FishInformation Update(FishInformation item)
        {
            FishInformation fish = _context.FishInformation.Where(b => b.FishId == item.FishId).FirstOrDefault();
            if (fish != null)
            {
                fish.FishId = item.FishId;
                fish.FishName = item.FishName;
            }
            _context.SaveChanges();

            return fish;
        }
       
        
        public IEnumerable<FishInformation> GetAll(int id)
        {
            return _context.FishInformation.Where(rec => rec.FishId == id).ToList();
        }

        public IEnumerable<String> Search(string searchString)
        {
                 var result =GetAll()
                 .Where(fish => fish.FishName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                 .Select(fish => fish.FishName)
                 .Take(10)
                 .ToList();

            return result;
        }
    }
}
