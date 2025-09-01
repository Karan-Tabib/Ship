
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.Models
{
    public class CrewInformation
    {
        public required int CrewID { get; set; }
        public required string Firstname { get; set; }
        public required string Middlename { get; set; }
        public required string Lastname { get; set; }
        public required string AdharNo { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public DateTime DOB { get; set; }

        //Navigation Property

        #region Boat to Crewmember(one-to-Many)
        /* Boat to Crewmember(one-to-Many)
         * A Single Boat can have multiple Crew Members.
         * Each member is associated with single Boat
         * This is classic one-to-many relationship.
         */
        [ForeignKey("BoatId")]
        public int BoatId { get; set; }
        public virtual BoatInformation Boat { get; set; }   // one crew works on one boat

        #endregion


        #region Crew Member to SalaryInformation(one-to-one)
        /* Crew Member to SalaryInformation(one-to-one)
         * A Single crew menber can have only one Salary record.
         * Each salary record is associated with single Crew Member
         * This is classic one-to-one relationship.
         */
        public virtual SalaryInformation? SalaryInfo { get; set; }
        #endregion


        #region Crew Member to LeaveInformation(one-to-one)
        /* Crew Member to LeaveInformation(one-to-one)
         * A Single crew menber can have only one record of Available Leaves.
         * Each leave record is associated with single Crew Member
         * This is classic one-to-one relationship.
         */
        public virtual LeaveInformation? LeaveInfo { get; set; }
        #endregion

        #region Crew to LeaveSummary(one-to-Many)
        /* Crew Member to LeaveSummary(one-to-Many)
         * A Single Crew can take multiple Leaves (leaves record).
         * Each leaves record is associated with single Crew Member
         * This is classic one-to-many relationship.
         */
        // Crew member can have multiple leave summary and salary summary
        public ICollection<LeaveSummary> LeaveSummaries { get; set; }
        #endregion

        #region Crew to SalarySummary(one-to-Many)
        /* Crew Member to SalarySummary(one-to-Many)
         * A Single Crew can withdraw multiple Payments (Paid record).
         * Each paid record is associated with single Crew Member
         * This is classic one-to-many relationship.
         */
        // Crew member can have multiple leave summary and salary summary
        public ICollection<SalarySummary> SalarySummaries { get; set; }
        #endregion
        
        public CrewInformation()
        {

        }
    }
}
