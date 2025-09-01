namespace DTOs.DTO
{
    public class TripDataDTO
    {
        public TripInfoDTO TripInfo { get; set; }
        public List<TripParticularDTO> ?TripParticulars { get; set; }
        public List<TripExpenditureDTO> ? TripExpenditures { get; set; }
    }
}
