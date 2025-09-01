namespace DTOs.DTO
{
    public class TripExpenditureDTO
    {
        public int TripExpenditureId { get; set; }
        public string Reason { get; set; }
        public int TripId { get; set; }
        public DateTime TripDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int BoatId { get; set; }
    }
}
