using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class FishBL : IFishBL
    {
        private IFishRepository _fishRepository;

        public FishBL(IFishRepository fishRepository)
        {
            _fishRepository = fishRepository;
        }

        public FishInfoDTO Add(FishInfoDTO item)
        {
            var newRec = GetFishInformationObject(item);
            newRec = _fishRepository.Add(newRec);

            var boatDTO = GetFishInformationDToObject(newRec);

            return boatDTO;
        }

        public FishInfoDTO Get(int fishId)
        {
            var fish = _fishRepository.Get(fishId);
            if (fish == null)
            {
                return null;
            }
            var fishDTo = GetFishInformationDToObject(fish);
            return fishDTo;
        }

        public IEnumerable<FishInfoDTO> GetAll(string userId)
        {
            var list = _fishRepository.GetAll(Convert.ToInt32(userId));
            List<FishInfoDTO> fishInfoList = new List<FishInfoDTO>();
            fishInfoList = list.Select(item => GetFishInformationDToObject(item)).ToList();

            return fishInfoList;
        }

        public IEnumerable<FishInfoDTO> GetAll()
        {
            var list = _fishRepository.GetAll();
            List<FishInfoDTO> fishInfoList = new List<FishInfoDTO>();
            fishInfoList = list.Select(item => GetFishInformationDToObject(item)).ToList();

            return fishInfoList;
        }

        public bool Remove(int boatId)
        {
            return _fishRepository.Remove(boatId);
        }

        public FishInfoDTO Update(FishInfoDTO item)
        {
            var newRec = GetFishInformationObject(item);
            _fishRepository.Update(newRec);

            return item;
        }

        public static FishInformation GetFishInformationObject(FishInfoDTO item)
        {
            FishInformation fish = new FishInformation()
            {
                FishName = item.FishName,
                FishId = item.FishId
            };

            return fish;
        }

        public static FishInfoDTO GetFishInformationDToObject(FishInformation item)
        {
            FishInfoDTO fish = new FishInfoDTO()
            {
                FishName = item.FishName,
                FishId = item.FishId
            };

            return fish;
        }

        public IEnumerable<string> Search(string searchString)
        {
            var list = _fishRepository.Search(searchString);

            return list;
        }
    }
}
