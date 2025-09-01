using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class LeavesBL : ILeavesBL
    {
        private ILeaveRepository _leavesRepository;

        public LeavesBL(ILeaveRepository salaryRepository)
        {
            _leavesRepository = salaryRepository;
        }

        public LeaveInfoDTO Add(LeaveInfoDTO item)
        {
            var newRec = GetLeaveInfoObject(item);
            newRec = _leavesRepository.Add(newRec);
            var leaveDTO = GetLeaveInfoDTO(newRec);

            return leaveDTO;
        }

        public LeaveInfoDTO Get(int id)
        {
            try
            {
                var leave = _leavesRepository.Get(id);
                if (leave == null)
                {
                    return null;
                }
                var salaryDto = GetLeaveInfoDTO(leave);
                return salaryDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<LeaveBoatDTO> GetAll(string userId)
        {
            var list = _leavesRepository.GetAll(Convert.ToInt32(userId));

            //List<LeaveBoatDTO> salaryInfoList = new List<LeaveBoatDTO>();
            //salaryInfoList = list
            //        .Where(leave => leave != null) // Filter out null values
            //      .Select(leave => GetLeaveInfoDTO(leave)) // Transform items
            //    .ToList();

            return list;
        }

        public bool Remove(int id)
        {
            return _leavesRepository.Remove(id);
        }

        public LeaveInfoDTO Update(LeaveInfoDTO item)
        {
            var newRec = GetLeaveInfoObject(item);
            _leavesRepository.Update(newRec);
            return item;
        }

        private LeaveInformation GetLeaveInfoObject(LeaveInfoDTO item)
        {
            LeaveInformation newRec = new LeaveInformation()
            {
                LeaveId = item.LeaveId,
                CrewId = item.CrewId,
                ForYear = item.ForYear,
                TotalLeaves = item.TotalLeaves,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
            };
            return newRec;
        }

        private LeaveInfoDTO GetLeaveInfoDTO(LeaveInformation item)
        {
            LeaveInfoDTO newRec = new LeaveInfoDTO()
            {
                LeaveId = item.LeaveId,
                CrewId = item.CrewId,
                ForYear = item.ForYear,
                TotalLeaves = item.TotalLeaves,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
            };
            return newRec;
        }
    }
}
