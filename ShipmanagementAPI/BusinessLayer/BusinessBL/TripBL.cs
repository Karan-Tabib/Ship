using BusinessLayer.Abstraction;
using DataLayer.Entities;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using ShipAPI.Models;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class TripBL : ITripBL
    {
        private ITripRepository _tripRepository;

        public TripBL(ITripRepository tripRepostiory)
        {
            _tripRepository = tripRepostiory;
        }

        public TripDataDTO Add(TripDataDTO item)
        {
            TripInformation tripinfo = TripDataUtility.GetTripIformationObject(item.TripInfo);
            List<TripParticular> tripparticular = TripDataUtility.GetTripParticularObject(item.TripParticulars);
            List<TripExpenditure> tripExpo = TripDataUtility.GetTripExpenditureObject(item.TripExpenditures);
            item = _tripRepository.Add(tripinfo, tripparticular, tripExpo);

            return item;
        }

        public TripDataDTO GetTripData(int tripId)
        {
            TripDataDTO data = _tripRepository.GetTripData(tripId);
            return data;
        }

        public List<TripInfoDTO> GetTripInformation(int tripId)
        {
            List<TripInfoDTO> data = _tripRepository.GetTripInformation(tripId);
            return data;
        }

        public List<TripParticularDTO> GetTripParticulars(int tripId)
        {
            List<TripParticularDTO> data = _tripRepository.GetTripParticulars(tripId);
            return data;
        }
        public List<TripExpenditureDTO> GetTripExpenditures(int tripId)
        {
            List<TripExpenditureDTO> data = _tripRepository.GetTripExpenditures(tripId);
            return data;
        }

        public async Task<bool> UpdateTripData(TripDataDTO tripData)
        {
            return await _tripRepository.UpdateTripData(tripData);
        }

        public async Task<bool> DeleteTripData(int tripId)
        {
            return await _tripRepository.DeleteTripData(tripId);
        }
    }
}
