using DataLayer.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public static class TripDataUtility
    {
        public static TripInformation GetTripIformationObject(TripInfoDTO infoDTO)
        {
            if (infoDTO == null)
            {
                return null;
            }
            TripInformation tripInfo = new TripInformation()
            {
                TripId = infoDTO.TripId,
                TripName = infoDTO.TripName,
                TripStartDate = infoDTO.TripStartDate,
                TripEndDate = infoDTO.TripEndDate,
                BoatId = infoDTO.BoatId,
                TripDescription = infoDTO.TripDescription,
                CreatedDate = infoDTO.CreatedDate,
                UpdatedDate = infoDTO.UpdatedDate,
            };
            return tripInfo;
        }
        public static TripInfoDTO GetTripIformationDTOObject(TripInformation data)
        {
            if (data == null)
            {
                return null;
            }
            TripInfoDTO tripInfoDTO = new TripInfoDTO()
            {
                TripId = data.TripId,
                TripName = data.TripName,
                TripStartDate = data.TripStartDate,
                TripEndDate = data.TripEndDate,
                BoatId = data.BoatId,
                TripDescription = data.TripDescription,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
            };
            return tripInfoDTO;
        }
        
        public static List<TripParticular> GetTripParticularObject(List<TripParticularDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                return null;
            }

            List<TripParticular> particularList = new List<TripParticular>();

            particularList = data.Select(rec => new TripParticular()
            {
                TripParticularId = rec.TripParticularId,
                TripId = rec.TripId,
                TripDate = rec.TripDate,
                FishId = rec.FishId,
                RatePerKg = rec.RatePerKg,
                TotalWeight = rec.TotalWeight,
                Amount = rec.Amount,
                UpdatedDate = rec.UpdatedDate,
                CreatedDate = rec.CreatedDate,
            }).ToList();
            return particularList;
        }
        public static List<TripParticularDTO> GetTripParticularDTOObject(List<TripParticular> data)
        {
            if (data == null || data.Count == 0)
            {
                return null;
            }

            List<TripParticularDTO> particularDTOList = new List<TripParticularDTO>();

            particularDTOList = data.Select(rec => new TripParticularDTO()
            {
                TripParticularId = rec.TripParticularId,
                TripId = rec.TripId,
                TripDate = rec.TripDate,
                FishId = rec.FishId,
                RatePerKg = rec.RatePerKg,
                TotalWeight = rec.TotalWeight,
                Amount = rec.Amount,
                UpdatedDate = rec.UpdatedDate,
                CreatedDate = rec.CreatedDate,
            }).ToList();
            return particularDTOList;
        }
        public static void UpdateTripParticularData(TripParticular existingData, TripParticularDTO newData)
        {
            existingData.TripParticularId = newData.TripParticularId;
            existingData.TripId = newData.TripId;
            existingData.TripDate = newData.TripDate;
            existingData.FishId = newData.FishId;
            existingData.RatePerKg = newData.RatePerKg;
            existingData.TotalWeight = newData.TotalWeight;
            existingData.Amount = newData.Amount;
            existingData.UpdatedDate = newData.UpdatedDate;
            existingData.CreatedDate = newData.CreatedDate;
        }
        public static TripParticular GetTripParticularObject(TripParticularDTO rec)
        {
            if (rec == null)
            {
                return null;
            }

            TripParticular particularList = new TripParticular()
            {
                TripParticularId = rec.TripParticularId,
                TripId = rec.TripId,
                TripDate = rec.TripDate,
                FishId = rec.FishId,
                RatePerKg = rec.RatePerKg,
                TotalWeight = rec.TotalWeight,
                Amount = rec.Amount,
                UpdatedDate = rec.UpdatedDate,
                CreatedDate = rec.CreatedDate,
            };

            return particularList;
        }

        public static List<TripExpenditure> GetTripExpenditureObject(List<TripExpenditureDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                return null;
            }

            List<TripExpenditure> expenditure = new List<TripExpenditure>();

            expenditure = data.Select(rec => new TripExpenditure()
            {
                TripExpenditureId = rec.TripExpenditureId,
                TripId = rec.TripId,
                TripDate = rec.TripDate,
                Reason = rec.Reason,
                Amount = rec.Amount,
                UpdatedDate = rec.UpdatedDate,
                CreatedDate = rec.CreatedDate,
            }).ToList();
            return expenditure;
        }
        public static List<TripExpenditureDTO> GetTripExpenditureDToObject(List<TripExpenditure> data)
        {
            if (data == null || data.Count == 0)
            {
                return null;
            }

            List<TripExpenditureDTO> expenditureDTO = new List<TripExpenditureDTO>();

            expenditureDTO = data.Select(rec => new TripExpenditureDTO()
            {
                TripExpenditureId = rec.TripExpenditureId,
                TripId = rec.TripId,
                TripDate = rec.TripDate,
                Reason = rec.Reason,
                Amount = rec.Amount,
                UpdatedDate = rec.UpdatedDate,
                CreatedDate = rec.CreatedDate,
            }).ToList();
            return expenditureDTO;
        }
        public static void UpdateTripExpenditureData(TripExpenditure existingData, TripExpenditureDTO newData)
        {
            existingData.TripExpenditureId = newData.TripExpenditureId;
            existingData.TripId = newData.TripId;
            existingData.TripDate = newData.TripDate;
            existingData.Reason = newData.Reason;
            existingData.Amount = newData.Amount;
            existingData.UpdatedDate = newData.UpdatedDate;
        }
        public static TripExpenditure GetTripExpenditureObject(TripExpenditureDTO rec)
        {
            if (rec == null)
            {
                return null;
            }

            TripExpenditure expenditure = new TripExpenditure()
            {
                TripExpenditureId = rec.TripExpenditureId,
                TripId = rec.TripId,
                TripDate = rec.TripDate,
                Reason = rec.Reason,
                Amount = rec.Amount,
                UpdatedDate = rec.UpdatedDate,
                CreatedDate = rec.CreatedDate,
            };

            return expenditure;
        }
    }
}
