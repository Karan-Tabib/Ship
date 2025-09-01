using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class SalaryBl : ISalaryBL
    {
        private ISalaryRepository _salaryRepository;

        public SalaryBl(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public SalaryInfoDTO Add(SalaryInfoDTO item)
        {
            var newRec = new SalaryInformation()
            {
                CrewId = item.CrewId,
                ForYear = item.ForYear,
                TotalSalary = item.TotalSalary,
                startDate = item.startDate,
                endDate = item.endDate,
            };
            newRec = _salaryRepository.Add(newRec);

            var boatDTO = new SalaryInfoDTO()
            {
                CrewId = newRec.CrewId,
                ForYear = newRec.ForYear,
                TotalSalary = newRec.TotalSalary,
                startDate = newRec.startDate,
                endDate = newRec.endDate,
                Id = newRec.Id
            };

            return boatDTO;
        }

        public SalaryInfoDTO Get(int id)
        {
            try
            {
                var salary = _salaryRepository.Get(id);
                if (salary == null)
                {
                    return null;
                }
                var salaryDto = new SalaryInfoDTO()
                {
                    CrewId = salary.CrewId,
                    ForYear = salary.ForYear,
                    TotalSalary = salary.TotalSalary,
                    startDate = salary.startDate,
                    endDate = salary.endDate,
                    Id = salary.Id
                };
                return salaryDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<SalaryInfoDTO> GetAll(string userId)
        {
            var list = _salaryRepository.GetAll(Convert.ToInt32(userId));
            List<SalaryInfoDTO> salaryInfoList = new List<SalaryInfoDTO>();
            //foreach (var item in list)
            //{
            //    boatInfoList.Add(
            //    new SalaryInfoDTO()
            //    {
            //        BoatId = item.BoatId,
            //        BoatName = item.BoatName,
            //        BoatType = item.BoatType,
            //        UserId = item.UserId
            //    });
            //}
            salaryInfoList = list.Select(item => new SalaryInfoDTO()
            {
                CrewId = item.CrewId,
                ForYear = item.ForYear,
                TotalSalary = item.TotalSalary,
                startDate = item.startDate,
                endDate = item.endDate,
                Id = item.Id
            }).ToList();

            return list;
        }

        public bool Remove(int id)
        {
            //var exist = _boatRepository.Get(boatId);
            //if(exist == null)
            //{
            //    return false;
            //}
            return _salaryRepository.Remove(id);
        }

        public SalaryInfoDTO Update(SalaryInfoDTO item)
        {
            var newRec = new SalaryInformation()
            {
                CrewId = item.CrewId,
                ForYear = item.ForYear,
                TotalSalary = item.TotalSalary,
                startDate = item.startDate,
                endDate = item.endDate,
                Id = item.Id
            };
            _salaryRepository.Update(newRec);

            return item;
        }
    }
}
