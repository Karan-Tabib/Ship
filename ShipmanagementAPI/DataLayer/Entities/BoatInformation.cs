using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class BoatInformation
    {
        public  int BoatId { get; set; }

        public required string BoatName { get; set; }

        public required string BoatType { get; set; }

        //Navigation Property

        #region User to Boat (One-to-Many):
        /*
         * User to Boat (One-to-Many):
         * A single Owner can have multiple Boats (Boat records).
         * Each Boat is associated with one Onwer.
         * This is a classic one-to-many relationship.
         */
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? UserInfo { get; set; }   // boat has one Owner
        #endregion

        #region Boat to Crewmember(one-to-Many)
        /* Boat to Crewmember(one-to-Many)
         * A Single Boat can have multiple Crew Members.
         * Each member is associated with single Boat
         * This is classic one-to-many relationship.
         */
        public ICollection<CrewInformation> CrewMembers  { get; set; }  // one boat has many crew members

        #endregion

        #region Boat to TripInfo (One-to-Many):
        /*
         * Boat to TripInfo (One-to-Many):
         * A single Boat can have multiple trips (TripInfo records).
         * Each TripInfo is associated with one Boat.
         * This is a classic one-to-many relationship.
         */
        public ICollection<TripInformation> TripInfos { get; set; }

        #endregion
        
        public BoatInformation()
        {
        }

    }
}
