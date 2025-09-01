using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DTOs.Models
{
    public class LeaveSummary
    {
        public required int LeaveSummaryId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int NoOfDaysOff { get; set; }
        public required string Description { get; set; }

        #region Crew to LeaveSummary(one-to-Many)
        /* Crew Member to LeaveSummary(one-to-Many)
         * A Single Crew can take multiple Leaves (leaves record).
         * Each leaves record is associated with single Crew Member
         * This is classic one-to-many relationship.
         */
        [ForeignKey("CrewId")]
        public int CrewId { get; set; }
        public CrewInformation? Crew { get; set; }
        #endregion

        public LeaveSummary()
        {
            
        }
    }
}
