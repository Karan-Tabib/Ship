using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.Models
{
    public class SalarySummary
    {
        public required int SalarySummaryId { get; set; }
        public required decimal AmountTaken {  get; set; }
        public required string Description { get; set; }
        public required DateTime receivedDate {  get; set; }

        // Navigation properties

        #region Crew to SalarySummary(one-to-Many)
        /* Crew Member to SalarySummary(one-to-Many)
         * A Single Crew can withdraw multiple Payments (Paid record).
         * Each paid record is associated with single Crew Member
         * This is classic one-to-many relationship.
         */
        // Crew member can have multiple leave summary and salary summary
        //Foreign Key
        [ForeignKey("CrewId")]
        public int CrewId { get; set; }
        public CrewInformation? Crew { get; set; }  // This will hold the related Crew entity
        #endregion

        public SalarySummary()
        {
            
        }

    }
}
