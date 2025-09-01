using DataLayer.Entities;
using DataLayer.EntityFramework;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly ShipManagemntDbContext _Context;

        public LeaveRepository(ShipManagemntDbContext context) 
        { 
            _Context = context;
        }
        public LeaveInformation Add(LeaveInformation item)
        {
            _Context.LeaveInformation.Add(item);
            _Context.SaveChanges();
            return item;
        }

        public LeaveInformation Get(int id)
        {
            LeaveInformation? leave = null;
            leave =_Context.LeaveInformation.FirstOrDefault(rec => rec.LeaveId == id);
            return leave;
        }

        public IEnumerable<LeaveBoatDTO> GetAll(int id)
        {
            var result =
                from s in _Context.LeaveInformation
                join c in _Context.CrewInformation on s.CrewId equals c.CrewID
                join b in _Context.BoatInformation on c.BoatId equals b.BoatId
                where b.UserId == id
                select new LeaveBoatDTO()
                {

                    CrewId = s.CrewId,
                    ForYear = s.ForYear,
                    TotalLeaves = s.TotalLeaves,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    LeaveId = s.LeaveId,
                    BoatId = c.BoatId
                };  // This selects the SalaryInformation object itself

            return result.ToList(); // ToList() is necessary to materialize the result
        }


        public IEnumerable<LeaveInformation> GetAll()
        {
            IEnumerable<LeaveInformation> leaves = _Context.LeaveInformation.ToList();
            return leaves;
        }

        public bool Remove(int item)
        {
            var recordtodelete = Get(item);
            if (recordtodelete == null)
            {
                return false;
            }
            _Context.LeaveInformation.Remove(recordtodelete);
            _Context.SaveChanges();
            return true;
        }

        public LeaveInformation Update(LeaveInformation item)
        {
            LeaveInformation salary = _Context.LeaveInformation.Where(b => b.LeaveId == item.LeaveId).FirstOrDefault();
            if (salary != null)
            {
                salary.CrewId = item.CrewId;
                salary.ForYear = item.ForYear;
                salary.TotalLeaves = item.TotalLeaves;
                salary.StartDate = item.StartDate;
                salary.EndDate = item.EndDate;
                salary.LeaveId = item.LeaveId;
            }
            _Context.SaveChanges();

            return salary;
        }
    }
}
