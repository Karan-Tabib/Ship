using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class LeavesSummaryBl : ILeaveSummaryBL
    {
        private ILeaveSummaryRepository _leavesSummaryRepository;

        public LeavesSummaryBl(ILeaveSummaryRepository leaveSummary)
        {
            _leavesSummaryRepository = leaveSummary;
        }

        public LeaveSummaryDTO Add(LeaveSummaryDTO item)
        {
            var newRec = GetLeaveSummaryObject(item);
            newRec = _leavesSummaryRepository.Add(newRec);
            var salarySumDTo = GetLeaveSummaryDTOObject(newRec);

            return salarySumDTo;
        }

        public LeaveSummaryDTO Get(int id)
        {
            try
            {
                var newRec = _leavesSummaryRepository.Get(id);
                if (newRec == null)
                {
                    return null;
                }
                var salaryDto = GetLeaveSummaryDTOObject(newRec);
                return salaryDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<LeaveSummaryDTO> GetAll(string userId)
        {
            var list = _leavesSummaryRepository.GetAll(Convert.ToInt32(userId));
            List<LeaveSummaryDTO> salaryInfoList = new List<LeaveSummaryDTO>();

            //salaryInfoList = list.Select(newRec => new LeaveSummaryDTO()
            //{
            //    CrewId = newRec.CrewId,
            //    NoOfDaysOff = newRec.NoOfDaysOff,
            //    Description = newRec.Description,
            //    StartDate = newRec.StartDate,
            //    EndDate = newRec.EndDate,
            //    CreatedDate = newRec.CreatedDate,
            //    UpdatedDate = newRec.UpdatedDate,
            //    LeaveSummaryId = newRec.LeaveSummaryId,
            //    LeaveId = newRec.LeaveId
            //}).ToList();

            return list;
        }

        public bool Remove(int id)
        {
            return _leavesSummaryRepository.Remove(id);
        }

        public LeaveSummaryDTO Update(LeaveSummaryDTO item)
        {
            var newRec = GetLeaveSummaryObject(item);
            _leavesSummaryRepository.Update(newRec);

            return item;
        }

        public static LeaveSummary GetLeaveSummaryObject(LeaveSummaryDTO item)
        {
            var newRec = new LeaveSummary()
            {
                CrewId = item.CrewId,
                NoOfDaysOff = item.NoDaysOff,
                Description = item.Description,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                //LeaveId = item.LeaveId,
                LeaveSummaryId = item.LeaveSummaryId,
            };
            return newRec;
        }
        public static LeaveSummaryDTO GetLeaveSummaryDTOObject(LeaveSummary item)
        {
            var newRec = new LeaveSummaryDTO()
            {
                CrewId = item.CrewId,
                NoDaysOff = item.NoOfDaysOff,
                Description = item.Description,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                LeaveId = item.LeaveId,
                LeaveSummaryId = item.LeaveSummaryId,
            };
            return newRec;
        }
    }
}
