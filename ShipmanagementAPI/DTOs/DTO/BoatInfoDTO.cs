using System.ComponentModel.DataAnnotations;

namespace DTOs.DTO
{
    public class BoatInfoDTO
    {
        public required int BoatId { get; set; }

        public required string BoatName { get; set; }

        public required string BoatType { get; set; }

        public required int UserId { get; set; }
    }
}
