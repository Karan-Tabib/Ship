using DataLayer.Entities;
using DTOs.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ITripRepository
    {

        // Method to add a new item
        TripDataDTO Add(TripInformation tripInfoData, List<TripParticular> tripParticulars, List<TripExpenditure> tripExpenditures);

        List<TripInfoDTO> GetTripInformation(int boatId);

        TripDataDTO GetTripData(int tripId);
        List<TripParticularDTO> GetTripParticulars(int tripId);
        List<TripExpenditureDTO> GetTripExpenditures(int tripId);

        Task<bool> UpdateTripData(TripDataDTO tripData);
        Task<bool> DeleteTripData(int tripId);
    }

}
