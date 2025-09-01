namespace DTOs.DTO
{
    public class SalarySummaryDTO
    {
        public required int SalarySummaryId { get; set; }
        public required decimal AmountTaken { get; set; }
        public required string Description { get; set; }
        public required DateTime ReceivedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public required DateTime UpdatedDate { get; set; }
        public required int CrewId { get; set; }
        public required int SalaryId { get; set; }
    }
}
