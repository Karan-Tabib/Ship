using System.ComponentModel.DataAnnotations;

namespace DTOs.DTO
{
    public class SupplierInfoDTO    {

        public int SupplierId { get; set; }
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(50)]
        public required string Firstname { get; set; }

        public required string Middlename { get; set; }

        [Required(ErrorMessage = "LastName is Required!")]
        [StringLength(50)]
        public required string Lastname { get; set; }

        [Required(ErrorMessage = "Email Id Is Required")]
        [EmailAddress(ErrorMessage = "Email Address is not in Proper format")]
        public required string Email { get; set; }  // Primary Key

        [Phone]
        public required string Phone { get; set; }

        public required string Address { get; set; }

        public SupplierInfoDTO()
        {

        }
        
    }
}
