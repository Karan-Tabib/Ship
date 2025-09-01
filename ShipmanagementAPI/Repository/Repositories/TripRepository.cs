
using DataLayer.Entities;
using DataLayer.EntityFramework;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Repository.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly ShipManagemntDbContext _context;

        public TripRepository(ShipManagemntDbContext context)
        {
            _context = context;
        }

        public TripDataDTO Add(TripInformation tripInfoData, List<TripParticular> tripParticulars, List<TripExpenditure> tripExpenditures)
        {
            TripDataDTO tripData = new TripDataDTO();

            if (tripInfoData == null)
            {
                return null;
            }
            else
            {
                try
                {
                    _context.Database.BeginTransaction();
                    _context.TripInformation.Add(tripInfoData);
                    _context.SaveChanges();

                    if (tripParticulars != null && tripParticulars.Any())
                    {
                        tripParticulars.ForEach(tripParticular => tripParticular.TripId = tripInfoData.TripId);
                        _context.TripParticulars.AddRange(tripParticulars);

                    }
                    if (tripExpenditures != null && tripExpenditures.Any())
                    {
                        tripExpenditures.ForEach(exp => exp.TripId = tripInfoData.TripId);
                        _context.TripExpenditures.AddRange(tripExpenditures);
                    }
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();


                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                }
                finally
                {
                    tripData.TripInfo = TripDataUtility.GetTripIformationDTOObject(tripInfoData);
                    tripData.TripParticulars = tripParticulars != null ? TripDataUtility.GetTripParticularDTOObject(tripParticulars) : null;  // Add tripParticulars to the DTO
                    tripData.TripExpenditures = tripExpenditures != null ? TripDataUtility.GetTripExpenditureDToObject(tripExpenditures) : null;
                }
            }

            return tripData;
        }

        public TripDataDTO GetTripData(int tripID)
        {
            TripDataDTO tripData = new TripDataDTO();
            List<TripParticular> tripParticulars = new List<TripParticular>();
            List<TripExpenditure> tripExpenditures = new List<TripExpenditure>();
            if (tripID == null)
            {
                return null;
            }
            else
            {
                try
                {
                    TripInformation tripInfoData = _context.TripInformation.FirstOrDefault(rec => rec.TripId == tripID);

                    if (tripInfoData == null)
                    {
                        return null;
                    }
                    else
                    {
                        tripData.TripInfo = TripDataUtility.GetTripIformationDTOObject(tripInfoData);
                        tripParticulars = _context.TripParticulars.Where(trip => trip.TripId == tripID).ToList();
                        tripExpenditures = _context.TripExpenditures.Where(trip => trip.TripId == tripID).ToList();

                        if (tripParticulars != null && tripParticulars.Any())
                        {
                            tripData.TripParticulars = tripParticulars != null ? TripDataUtility.GetTripParticularDTOObject(tripParticulars) : null;  // Add tripParticulars to the DTO
                        }
                        if (tripExpenditures != null && tripExpenditures.Any())
                        {
                            tripData.TripExpenditures = tripExpenditures != null ? TripDataUtility.GetTripExpenditureDToObject(tripExpenditures) : null;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            }
            return tripData;
        }



        public List<TripInfoDTO> GetTripInformation(int boatId)
        {
            List<TripInformation> tripInfoData = _context.TripInformation.Where(rec => rec.BoatId == boatId).ToList();
            List<TripInfoDTO> tripInfoDTOs = new List<TripInfoDTO>();
            tripInfoDTOs = tripInfoData.Select(rec => TripDataUtility.GetTripIformationDTOObject(rec)).ToList();
            return tripInfoDTOs;
        }
        public List<TripExpenditureDTO> GetTripExpenditures(int tripId)
        {
            List<TripExpenditure> tripParticularData = _context.TripExpenditures.Where(rec => rec.TripId == tripId).ToList();
            List<TripExpenditureDTO> tripInfoDTOs = new List<TripExpenditureDTO>();
            tripInfoDTOs = TripDataUtility.GetTripExpenditureDToObject(tripParticularData);
            return tripInfoDTOs;

        }

        public List<TripParticularDTO> GetTripParticulars(int tripId)
        {
            List<TripParticular> tripExpData = _context.TripParticulars.Where(rec => rec.TripId == tripId).ToList();
            List<TripParticularDTO> tripInfoDTOs = new List<TripParticularDTO>();
            tripInfoDTOs = TripDataUtility.GetTripParticularDTOObject(tripExpData);
            return tripInfoDTOs;
        }

        public async Task<bool> UpdateTripData(TripDataDTO tripData)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //1. Update tripInfo
                    var tripInfo = await _context.TripInformation.FindAsync(tripData.TripInfo.TripId);
                    if (tripInfo != null)
                    {
                        UpdateTripInfo(tripInfo, tripData.TripInfo);
                    }
                    //2. Update trip Particulars
                    foreach (var tripParticular in tripData.TripParticulars)
                    {

                        var existingParticular = await _context.TripParticulars.FindAsync(tripParticular.TripParticularId);
                        if (existingParticular != null)
                        {
                            TripDataUtility.UpdateTripParticularData(existingParticular, tripParticular);
                        }
                        else
                        {
                            var newEntry = TripDataUtility.GetTripParticularObject(tripParticular);
                            newEntry.TripId = tripInfo.TripId;
                            _context.TripParticulars.Add(newEntry);
                        }
                    }

                    //3. Update trip Expendtiures
                    foreach (var expeditures in tripData.TripExpenditures)
                    {

                        var existingExp = await _context.TripExpenditures.FindAsync(expeditures.TripExpenditureId);
                        if (existingExp != null)
                        {
                            TripDataUtility.UpdateTripExpenditureData(existingExp, expeditures);
                        }
                        else
                        {
                            var newEntry = TripDataUtility.GetTripExpenditureObject(expeditures);
                            newEntry.TripId = tripInfo.TripId;
                            _context.TripExpenditures.Add(newEntry);
                        }
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        private void UpdateTripInfo(TripInformation trip, TripInfoDTO tripInfoDTO)
        {
            trip.TripId = tripInfoDTO.TripId;
            trip.TripName = tripInfoDTO.TripName;
            trip.TripStartDate = tripInfoDTO.TripStartDate;
            trip.TripEndDate = tripInfoDTO.TripEndDate;
            trip.TripDescription = tripInfoDTO.TripDescription;
            trip.TripId = tripInfoDTO.TripId;
            trip.UpdatedDate = tripInfoDTO.UpdatedDate;
        }

        public async Task<bool> DeleteTripData(int tripId)
        {
            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingrec =await _context.TripInformation.FindAsync(tripId);
                    if (existingrec != null) { 

                        _context.TripInformation.Remove(existingrec);
                        await _context.SaveChangesAsync();
                        await _context.Database.CommitTransactionAsync();
                        return true;
                    }
                }
                catch (Exception ex) { 
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            return false;
        }
    }

}
