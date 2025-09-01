using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace DataLayer.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int UserId { get; set; }

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

        public string? Password { get; set; }

        [Phone]
        public required string Phone { get; set; }

        public required string Address { get; set; }

        //Navigation propety

        #region   User to Boat (One-to-Many):
        /*
         * User to Boat (One-to-Many):
         * A single Owner can have multiple Boats (Boat records).
         * Each Boat is associated with one Onwer.
         * This is a classic one-to-many relationship.
         */
        public ICollection<BoatInformation> Boats { get; set; }    // One Owner has many boats
        #endregion

        public User()
        {

        }
        
    }
}

