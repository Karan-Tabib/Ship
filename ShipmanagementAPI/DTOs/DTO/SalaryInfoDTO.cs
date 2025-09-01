namespace DTOs.DTO
{
    public class SalaryInfoDTO
    {
        public int Id { get; set; }
        public Decimal TotalSalary { get; set; }
        public int ForYear { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int CrewId { get; set; }
        public int BoatId { get; set; }
    }
}
