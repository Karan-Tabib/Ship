using DTOs.DTO;
using System.Linq.Expressions;

namespace BusinessLayer.Abstraction
{
    public interface ITripBL
    {
        TripDataDTO Add(TripDataDTO item);
        TripDataDTO GetTripData(int tripId);
        List<TripInfoDTO> GetTripInformation(int tripId);
        List<TripParticularDTO> GetTripParticulars(int tripId);
        List<TripExpenditureDTO> GetTripExpenditures(int tripId);
        Task<bool> UpdateTripData(TripDataDTO tripData);
        Task<bool> DeleteTripData(int tripId);
    }
}