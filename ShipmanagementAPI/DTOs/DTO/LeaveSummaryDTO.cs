namespace DTOs.DTO
{
    public class LeaveSummaryDTO
    {
        public  int LeaveSummaryId { get; set; }
        public  DateTime StartDate { get; set; }
        public  DateTime EndDate { get; set; }
        public  int NoDaysOff { get; set; }
        public  string Description { get; set; }
        public int CrewId { get; set; }
        public int LeaveId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
