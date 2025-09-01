using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class BoatBL : IBoatBL
    {
        private IBoatRepository _boatRepository;

        public BoatBL(IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }

        public BoatInfoDTO Add(BoatInfoDTO item)
        {
            var newRec = new BoatInformation()
            {
                BoatName = item.BoatName,
                BoatType = item.BoatType,
                UserId = item.UserId,
            };
            newRec = _boatRepository.Add(newRec);

            var boatDTO = new BoatInfoDTO()
            {
                BoatId = newRec.BoatId,
                BoatName = newRec.BoatName,
                BoatType = newRec.BoatType,
                UserId = newRec.BoatId,
            };

            return boatDTO;
        }

        public BoatInfoDTO Get(int boatId)
        {
            try
            {
                var boat = _boatRepository.Get(boatId);
                if (boat == null)
                {
                    return null;
                }
                var boatDTO = new BoatInfoDTO()
                {
                    BoatId = boat.BoatId,
                    BoatName = boat.BoatName,
                    BoatType = boat.BoatType,
                    UserId = boat.BoatId,
                };
                return boatDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<BoatInfoDTO> GetAll(string userId)
        {
            var list = _boatRepository.GetAll(Convert.ToInt32(userId));
            List<BoatInfoDTO> boatInfoList = new List<BoatInfoDTO>();
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
            boatInfoList = list.Select(item => new BoatInfoDTO()
            {
                BoatId = item.BoatId,
                BoatName = item.BoatName,
                BoatType = item.BoatType,
                UserId = item.UserId
            }).ToList();

            return boatInfoList;
        }

        public bool Remove(int boatId)
        {
            //var exist = _boatRepository.Get(boatId);
            //if(exist == null)
            //{
            //    return false;
            //}
            return _boatRepository.Remove(boatId);
        }

        public BoatInfoDTO Update(BoatInfoDTO item)
        {
            var newRec = new BoatInformation()
            {
                BoatId = item.BoatId,
                BoatName = item.BoatName,
                BoatType = item.BoatType,
                UserId = item.UserId,
            };
           _boatRepository.Update(newRec);

            return item;
        }
    }
}
