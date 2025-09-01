using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class SalarySummaryBl : ISalarySummaryBL
    {
        private ISalarySummaryRepository _salarySummaryRepository;

        public SalarySummaryBl(ISalarySummaryRepository salaryRepository)
        {
            _salarySummaryRepository = salaryRepository;
        }

        public SalarySummaryDTO Add(SalarySummaryDTO item)
        {
            var newRec = new SalarySummary()
            {
                CrewId = item.CrewId,
                AmountTaken = item.AmountTaken,
                Description = item.Description,
                ReceivedDate = item.ReceivedDate,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                SalaryId = item.SalaryId

            };
            newRec = _salarySummaryRepository.Add(newRec);

            var salarySumDTo = new SalarySummaryDTO()
            {
                CrewId = newRec.CrewId,
                AmountTaken = newRec.AmountTaken,
                Description = newRec.Description,
                ReceivedDate = newRec.ReceivedDate,
                CreatedDate = newRec.CreatedDate,
                UpdatedDate = newRec.UpdatedDate,
                SalarySummaryId = newRec.SalarySummaryId,
                SalaryId = newRec.SalaryId
            };

            return salarySumDTo;
        }

        public SalarySummaryDTO Get(int id)
        {
            try
            {
                var newRec = _salarySummaryRepository.Get(id);
                if (newRec == null)
                {
                    return null;
                }
                var salaryDto = new SalarySummaryDTO()
                {
                    CrewId = newRec.CrewId,
                    AmountTaken = newRec.AmountTaken,
                    Description = newRec.Description,
                    ReceivedDate = newRec.ReceivedDate,
                    CreatedDate = newRec.CreatedDate,
                    UpdatedDate = newRec.UpdatedDate,
                    SalarySummaryId = newRec.SalarySummaryId,
                    SalaryId =newRec.SalaryId
                };
                return salaryDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<SalarySummaryDTO> GetAll(string userId)
        {
            var list = _salarySummaryRepository.GetAll(Convert.ToInt32(userId));
            List<SalarySummaryDTO> salaryInfoList = new List<SalarySummaryDTO>();
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
            salaryInfoList = list.Select(newRec => new SalarySummaryDTO()
            {
                CrewId = newRec.CrewId,
                AmountTaken = newRec.AmountTaken,
                Description = newRec.Description,
                ReceivedDate = newRec.ReceivedDate,
                CreatedDate = newRec.CreatedDate,
                UpdatedDate = newRec.UpdatedDate,
                SalarySummaryId = newRec.SalarySummaryId,
                SalaryId=newRec.SalaryId
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
            return _salarySummaryRepository.Remove(id);
        }

        public SalarySummaryDTO Update(SalarySummaryDTO item)
        {
            var newRec = new SalarySummary()
            {
                CrewId = item.CrewId,
                AmountTaken = item.AmountTaken,
                Description = item.Description,
                ReceivedDate = item.ReceivedDate,
                //CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                SalarySummaryId= item.SalarySummaryId,
            };
            _salarySummaryRepository.Update(newRec);

            return item;
        }
    }
}
