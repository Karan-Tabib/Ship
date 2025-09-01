using System.ComponentModel.DataAnnotations;

namespace   DTOs.DTO
{
    public class LoginResponseDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
