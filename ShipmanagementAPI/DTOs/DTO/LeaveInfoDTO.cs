namespace DTOs.DTO
{
    public class LeaveInfoDTO
    {
        public int LeaveId { get; set; }
        public int CrewId { get; set; }
        public int TotalLeaves { get; set; }
        public int ForYear { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        public LeaveInfoDTO() { }
    }

    public class LeaveBoatDTO : LeaveInfoDTO
    {
        public int BoatId { get; set; } 
    }
}
