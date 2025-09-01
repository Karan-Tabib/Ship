using System.Net;
using System.Reflection;

namespace DTOs.DTO
{
    public class CrewInfoDTO
    {
        public int CrewID { get; set; }
        public required string Firstname { get; set; }
        public required string Middlename { get; set; }
        public required string Lastname { get; set; }
        public required string AdharNumber { get; set; }
        public DateTime DOB { get; set; }
        public required string Address { get; set; }
        public required string Gender { get; set; }
        public required string BoatId { get; set; }
        public required string MobileNumber { get; set; }

        public CrewInfoDTO()
        {

        }
    }
}
