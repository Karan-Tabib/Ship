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
    public class SalaryRepository : ISalaryRepository
    {
        private readonly ShipManagemntDbContext _Context;

        public SalaryRepository(ShipManagemntDbContext context) 
        { 
            _Context = context;
        }
        public SalaryInformation Add(SalaryInformation item)
        {
            _Context.CrewSalaryInformation.Add(item);
            _Context.SaveChanges();
            return item;
        }

        public SalaryInformation Get(int id)
        {
            SalaryInformation? salary = null;
            salary =_Context.CrewSalaryInformation.FirstOrDefault(salary => salary.Id == id);
            return salary;
        }

        public IEnumerable<SalaryInfoDTO> GetAll(int id)
        {
            var result =
                from s in _Context.CrewSalaryInformation
                join c in _Context.CrewInformation on s.CrewId equals c.CrewID
                join b in _Context.BoatInformation on c.BoatId equals b.BoatId
                where b.UserId == id
                select new SalaryInfoDTO
                {

                    CrewId = s.CrewId,
                    ForYear = s.ForYear,
                    TotalSalary = s.TotalSalary,
                    startDate = s.startDate,
                    endDate = s.endDate,
                    Id = s.Id,
                    BoatId =c.BoatId
                };  // This selects the SalaryInformation object itself

            return result.ToList(); // ToList() is necessary to materialize the result
        }


        public IEnumerable<SalaryInformation> GetAll()
        {
            IEnumerable<SalaryInformation> salaries = _Context.CrewSalaryInformation.ToList();
            return salaries;
        }

        public bool Remove(int item)
        {
            var recordtodelete = Get(item);
            if (recordtodelete == null)
            {
                return false;
            }
            _Context.CrewSalaryInformation.Remove(recordtodelete);
            _Context.SaveChanges();
            return true;
        }

        public SalaryInformation Update(SalaryInformation item)
        {
            SalaryInformation salary = _Context.CrewSalaryInformation.Where(b => b.Id == item.Id).FirstOrDefault();
            if (salary != null)
            {
                salary.CrewId = item.CrewId;
                salary.ForYear = item.ForYear;
                salary.TotalSalary = item.TotalSalary;
                salary.startDate = item.startDate;
                salary.endDate = item.endDate;
            }
            _Context.SaveChanges();

            return salary;
        }
    }
}
