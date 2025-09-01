
using DataLayer.Entities;
using DataLayer.EntityFramework;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class BoatRepository : IBoatRepository
    {
        private readonly ShipManagemntDbContext _context;

        public BoatRepository(ShipManagemntDbContext context)
        {
            _context = context;
        }

        public BoatInformation Add(BoatInformation item)
        {
            try
            {
                _context.Database.BeginTransaction();
                _context.BoatInformation.Add(item);
                _context.SaveChanges();
                _context.Database.CommitTransaction();
                //item.BoatId = newRec.BoatId;
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
            }
            return item;
        }

        public BoatInformation Get(int boatId)
        {
            BoatInformation? boat = null;
            try
            {
              //  _context.Database.BeginTransaction();
                boat = _context.BoatInformation.Where(boat => boat.BoatId == boatId).FirstOrDefault();
               // _context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_context.Database.RollbackTransaction();
            }
            return boat;
        }
        
        public bool Remove(int boatId)
        {
            try
            {
                _context.Database.BeginTransaction();
                var recordtodelete = Get(boatId);
                if(recordtodelete == null)
                {
                    return false;
                }
                _context.BoatInformation.Remove(recordtodelete);
                _context.SaveChanges();
                _context.Database.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return false;
            }
        }

        public BoatInformation Update(BoatInformation item)
        {
            try
            {
               
                _context.Database.BeginTransaction();

                // First Way To Update 
                BoatInformation boat = _context.BoatInformation.Where(b => b.BoatId == item.BoatId).FirstOrDefault();
                if (boat != null)
                {
                    boat.BoatName = item.BoatName;
                    boat.BoatType = item.BoatType;
                    item.BoatName = boat.BoatName;
                    item.BoatType = boat.BoatType;
                }
                _context.SaveChanges();
                
                // second Way to Update
                //_context.Attach(item);
                //_context.Entry(item).State = EntityState.Modified;


                //_context.BoatInformation.Where(boat => boat.BoatId == item.BoatId).ExecuteUpdate(update =>
                //update.SetProperty(boat.))

                _context.Database.CommitTransaction();

            }catch(Exception ex)
            {
                _context.Database.RollbackTransaction();
            }
            
            return item;
        }

        public IEnumerable<BoatInformation> GetAll(int userId)
        {
            IEnumerable<BoatInformation>? boatInfos = null;
            try
            {
                _context.Database.BeginTransaction();
                boatInfos = _context.BoatInformation.Where(boat => boat.UserId == userId).ToList();
                _context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
            }

            return boatInfos;
        }

        public IEnumerable<BoatInformation> GetAll()
        {
            IEnumerable<BoatInformation>? boatInfos = null;
            try
            {
                _context.Database.BeginTransaction();
                boatInfos = _context.BoatInformation.ToList();
                _context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
            }

            return boatInfos;
        }

        
    }
}
