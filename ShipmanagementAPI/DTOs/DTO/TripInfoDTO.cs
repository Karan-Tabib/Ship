namespace DTOs.DTO
{
    public class TripInfoDTO
    {
        public required int TripId { get; set; }
        public required DateTime TripStartDate  { get; set;}
        public required DateTime TripEndDate { get; set;}
        public required string TripDescription { get; set; }
        public required int BoatId { get; set; }
        public string TripName { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdatedDate { get; set; }
        public TripInfoDTO()
        {
            
        }

    }
}
