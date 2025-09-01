using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DTOs.Models
{
    public class TripInformation
    {
        [Key]
        public int TripId { get; set; }
        public required string TripName { get; set; }
        public required DateTime TripStartDate { get; set; }
        public required DateTime TripEndDate { get; set; }
        public string TripDescription { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdatedDate { get; set; }

        //Navigation Property

        /* Foreign key for Boat
         * Boat to TripInfo (One-to-Many):
         *      A single Boat can have multiple trips (TripInfo records).
         *      Each TripInfo is associated with one Boat.
         *      This is a classic one-to-many relationship.
         */
        [ForeignKey("BoatId")]
        public int BoatId { get; set; }
        public BoatInformation Boat { get; set; }


        /*
         * TripInfo to TripExpenditure (One-to-Many):
         *      Each TripInfo can have multiple TripExpenditure records.
         *      Each TripExpenditure is associated with one TripInfo.
         *      Again, this is a one-to-many relationship.
        */
        public ICollection<TripExpenditure> TripExpenditures { get; set; }


        /*
         * TripInfo to TripParticular (One-to-Many):
         *      Each TripInfo can have multiple TripParticular records.
         *      Each TripParticular is associated with one TripInfo.
         *      This is also a one-to-many relationship.
         */
        public ICollection<TripParticular> TripParticulars { get; set; }


        public TripInformation()
        {

        }
    }
}
