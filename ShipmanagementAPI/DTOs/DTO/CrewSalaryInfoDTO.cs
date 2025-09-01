namespace DTOs.DTO
{
    public class CrewSalaryInfoDTO
    {
        
        public int CrewId { get; set; }
      
        public int BoatId { get; set; }
        
        public Decimal TotalSalary { get; set;}

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public CrewSalaryInfoDTO()
        {
            
        }

    }
}
