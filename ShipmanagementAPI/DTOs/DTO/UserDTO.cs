using System.ComponentModel.DataAnnotations;

namespace DTOs.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Firstname { get; set; }

        public required string Middlename { get; set; }

        [Required]
        [StringLength(50)]
        public required string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public required  string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        
        [Phone]
        public required string Phone { get; set; }
        public required string Address { get; set; }

        public UserDTO()
        {
            
        }
    }
}
