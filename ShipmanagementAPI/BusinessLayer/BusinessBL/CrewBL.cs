using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class CrewBL : ICrewBL
    {
        private ICrewRepository _crewRepository;

        public CrewBL(ICrewRepository crewRepository)
        {
            _crewRepository = crewRepository;
        }

        public CrewInfoDTO Add(CrewInfoDTO item)
        {
            var newRec = new CrewInformation()
            {
                Firstname = item.Firstname,
                Middlename = item.Middlename,
                Lastname = item.Lastname,
                Address = item.Address,
                DOB = item.DOB,
                Phone = item.MobileNumber,
                AdharNo = item.AdharNumber,
                BoatId = Convert.ToInt32(item.BoatId),
                CrewID = item.CrewID,
                Gender = (Gender)Enum.Parse(typeof(Gender),item.Gender),
            };
            newRec = _crewRepository.Add(newRec);

            var boatDTO = new CrewInfoDTO()
            {
                Firstname = newRec.Firstname,
                Middlename = newRec.Middlename,
                Lastname = newRec.Lastname,
                Address = newRec.Address,
                DOB = newRec.DOB,
                MobileNumber = newRec.Phone,
                AdharNumber = newRec.AdharNo,
                BoatId =newRec.BoatId.ToString(),
                CrewID = newRec.CrewID,
                Gender = newRec.Gender.ToString(),
            };

            return boatDTO;
        }

        public CrewInfoDTO Get(int crewId)
        {
            try
            {
                var newRec = _crewRepository.Get(crewId);
                if (newRec == null)
                {
                    return null;
                }
                var boatDTO = new CrewInfoDTO()
                {
                    Firstname = newRec.Firstname,
                    Middlename = newRec.Middlename,
                    Lastname = newRec.Lastname,
                    Address = newRec.Address,
                    DOB = newRec.DOB,
                    MobileNumber = newRec.Phone,
                    AdharNumber = newRec.AdharNo,
                    BoatId = newRec.BoatId.ToString(),
                    CrewID = newRec.CrewID,
                    Gender = newRec.Gender.ToString(),
                };
                return boatDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<CrewInfoDTO> GetAll(string userId)
        {
            var list = _crewRepository.GetAll(Convert.ToInt32(userId));
            List<CrewInfoDTO> boatInfoList = new List<CrewInfoDTO>();
            //foreach (var item in list)
            //{
            //    boatInfoList.Add(
            //    new BoatInfoDTO()
            //    {
            //        BoatId = item.BoatId,
            //        BoatName = item.BoatName,
            //        BoatType = item.BoatType,
            //        UserId = item.UserId
            //    });
            //}
            boatInfoList = list.Select(newRec => new CrewInfoDTO()
            {
                Firstname = newRec.Firstname,
                Middlename = newRec.Middlename,
                Lastname = newRec.Lastname,
                Address = newRec.Address,
                DOB = newRec.DOB,
                MobileNumber = newRec.Phone,
                AdharNumber = newRec.AdharNo,
                BoatId = newRec.BoatId.ToString(),
                CrewID = newRec.CrewID,
                Gender = newRec.Gender.ToString(),
            }).ToList();

            return boatInfoList;
        }

        public bool Remove(int CrewId)
        {
            //var exist = _boatRepository.Get(boatId);
            //if(exist == null)
            //{
            //    return false;
            //}
            return _crewRepository.Remove(CrewId);
        }

        public CrewInfoDTO Update(CrewInfoDTO item)
        {
            var newRec = new CrewInformation()
            {

                Firstname = item.Firstname,
                Middlename = item.Middlename,
                Lastname = item.Lastname,
                Address = item.Address,
                DOB = item.DOB,
                Phone = item.MobileNumber,
                AdharNo = item.AdharNumber,
                BoatId = Convert.ToInt32(item.BoatId),
                CrewID = item.CrewID,
                Gender = (Gender)Enum.Parse(typeof(Gender), item.Gender),
            };
            _crewRepository.Update(newRec);

            return item;
        }
    }
}
