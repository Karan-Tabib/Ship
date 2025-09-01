//using ShipAPI.Validators;

namespace ShipAPI.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }

       // [DateCheckAttribute]
        public DateTime AdmissionDate { get; set; }
        public StudentDTO()
        {
            
        }
    }
}
