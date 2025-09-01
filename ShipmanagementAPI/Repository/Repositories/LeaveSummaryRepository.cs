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
    public class LeaveSummaryRepository : ILeaveSummaryRepository
    {
        private readonly ShipManagemntDbContext _Context;

        public LeaveSummaryRepository(ShipManagemntDbContext context)
        {
            _Context = context;
        }
        public LeaveSummary Add(LeaveSummary item)
        {
            _Context.CrewLeaveSummary.Add(item);
            _Context.SaveChanges();
            return item;
        }

        public LeaveSummary Get(int id)
        {
            LeaveSummary? leaveSummary = null;
            leaveSummary = _Context.CrewLeaveSummary.FirstOrDefault(summary => summary.LeaveSummaryId == id);
            return leaveSummary;
        }

        public IEnumerable<LeaveSummaryDTO> GetAll(int id)
        {
            var result =
                from s in _Context.CrewLeaveSummary
                where s.CrewId == id
                select new LeaveSummaryDTO
                {

                    CrewId = s.CrewId,
                    NoDaysOff = s.NoOfDaysOff,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate,
                    LeaveSummaryId = s.LeaveSummaryId,
                    LeaveId = s.LeaveId
                };  // This selects the SalaryInformation object itself
            return result.ToList(); // ToList() is necessary to materialize the result
        }


        public IEnumerable<LeaveSummary> GetAll()
        {
            IEnumerable<LeaveSummary> salaries = _Context.CrewLeaveSummary.ToList();
            return salaries;
        }

        public bool Remove(int item)
        {
            var recordtodelete = Get(item);
            if (recordtodelete == null)
            {
                return false;
            }
            _Context.CrewLeaveSummary.Remove(recordtodelete);
            _Context.SaveChanges();
            return true;
        }

        public LeaveSummary Update(LeaveSummary item)
        {
            LeaveSummary salary = _Context.CrewLeaveSummary.Where(b => b.LeaveSummaryId == item.LeaveSummaryId).FirstOrDefault();
            if (salary != null)
            {
                salary.CrewId = item.CrewId;
                salary.NoOfDaysOff = item.NoOfDaysOff;
                salary.Description = item.Description;
                salary.StartDate = item.StartDate;
                salary.EndDate = item.EndDate;
                item.CreatedDate = salary.CreatedDate;
                salary.CreatedDate = salary.CreatedDate;
                salary.UpdatedDate = item.UpdatedDate;
                item.LeaveId = salary.LeaveId;
            }
            else
            {
                return item;
            }
            _Context.SaveChanges();

            return salary;
        }
    }
}
