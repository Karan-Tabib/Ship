
using DataLayer.Entities;
using DataLayer.EntityFramework;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class CrewRepository : ICrewRepository
    {
        private readonly ShipManagemntDbContext _context;

        public CrewRepository(ShipManagemntDbContext context)
        {
            _context = context;
        }

        public CrewInformation Add(CrewInformation item)
        {
            try
            {
                _context.Database.BeginTransaction();
                _context.CrewInformation.Add(item);
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
            }
            return item;
        }

        public CrewInformation Get(int crewId)
        {
            CrewInformation? crew = null;
            try
            {
              //  _context.Database.BeginTransaction();
                crew = _context.CrewInformation.Where(crew => crew.CrewID == crewId).FirstOrDefault();
               // _context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_context.Database.RollbackTransaction();
            }
            return crew;
        }
        
        public bool Remove(int crewId)
        {
            try
            {
                _context.Database.BeginTransaction();
                var recordtodelete = Get(crewId);
                if(recordtodelete == null)
                {
                    return false;
                }
                _context.CrewInformation.Remove(recordtodelete);
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

        public CrewInformation Update(CrewInformation item)
        {
            try
            {
               
                _context.Database.BeginTransaction();

                // First Way To Update 
                CrewInformation crewinfo = _context.CrewInformation.Where(b => b.CrewID == item.CrewID).FirstOrDefault();
                if (crewinfo != null)
                {
                    crewinfo.CrewID = item.CrewID;
                    crewinfo.Firstname = item.Firstname;
                    crewinfo.Middlename = item.Middlename;
                    crewinfo.Lastname = item.Lastname;
                    crewinfo.BoatId = item.BoatId ;
                    crewinfo.AdharNo = item.AdharNo;
                    crewinfo.Address = item.Address;
                    crewinfo.Phone = item.Phone;
                    crewinfo.DOB = item.DOB;
                   // crewinfo.ge = item.Lastname;

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

        public IEnumerable<CrewInformation> GetAll(int userId)
        {
            IEnumerable<CrewInformation>? crewInfos = null;
            try
            {
                // Fetch boats associated with the given user
                var boatList = _context.BoatInformation.Where(b => b.UserId == userId).ToList();

                // Extract Boat IDs
                var boatIds = boatList.Select(b => b.BoatId).ToList();

                // Fetch crew information for boats that match the IDs
                crewInfos = _context.CrewInformation.Where(c => boatIds.Contains(c.BoatId)).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

            return crewInfos;
        }

        public IEnumerable<CrewInformation> GetAll()
        {
            IEnumerable<CrewInformation>? boatInfos = null;
            try
            {
                _context.Database.BeginTransaction();
                boatInfos = _context.CrewInformation.ToList();
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
