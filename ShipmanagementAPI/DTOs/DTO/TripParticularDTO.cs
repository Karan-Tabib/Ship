using Azure.Core;

namespace DTOs.DTO
{
    public class TripParticularDTO
    {
        public int TripParticularId { get; set; }
        public int TripId { get; set; }
        public DateTime TripDate { get; set; }
        public int FishId { get; set; }
        public decimal RatePerKg { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int BoatId { get; set; }
    }
}

