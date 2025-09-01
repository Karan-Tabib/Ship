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
    public class SalarySummaryRepository : ISalarySummaryRepository
    {
        private readonly ShipManagemntDbContext _Context;

        public SalarySummaryRepository(ShipManagemntDbContext context)
        {
            _Context = context;
        }
        public SalarySummary Add(SalarySummary item)
        {
            _Context.CrewSalarySummary.Add(item);
            _Context.SaveChanges();
            return item;
        }

        public SalarySummary Get(int id)
        {
            SalarySummary? salary = null;
            salary = _Context.CrewSalarySummary.FirstOrDefault(salary => salary.SalarySummaryId == id);
            return salary;
        }

        public IEnumerable<SalarySummaryDTO> GetAll(int id)
        {
            var result =
                from s in _Context.CrewSalarySummary
                where s.CrewId == id
                select new SalarySummaryDTO
                {

                    CrewId = s.CrewId,
                    AmountTaken = s.AmountTaken,
                    Description = s.Description,
                    ReceivedDate = s.ReceivedDate,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate,
                    SalarySummaryId = s.SalarySummaryId,
                    SalaryId = s.SalaryId
                };  // This selects the SalaryInformation object itself
            return result.ToList(); // ToList() is necessary to materialize the result
        }


        public IEnumerable<SalarySummary> GetAll()
        {
            IEnumerable<SalarySummary> salaries = _Context.CrewSalarySummary.ToList();
            return salaries;
        }

        public bool Remove(int item)
        {
            var recordtodelete = Get(item);
            if (recordtodelete == null)
            {
                return false;
            }
            _Context.CrewSalarySummary.Remove(recordtodelete);
            _Context.SaveChanges();
            return true;
        }

        public SalarySummary Update(SalarySummary item)
        {
            SalarySummary salary = _Context.CrewSalarySummary.Where(b => b.SalarySummaryId == item.SalarySummaryId).FirstOrDefault();
            if (salary != null)
            {
                salary.CrewId = item.CrewId;
                salary.AmountTaken = item.AmountTaken;
                salary.Description = item.Description;
                salary.ReceivedDate = item.ReceivedDate;
                item.CreatedDate = salary.CreatedDate;
                salary.CreatedDate = salary.CreatedDate;
                salary.UpdatedDate = item.UpdatedDate;
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
