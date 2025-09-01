using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class LeaveInformation
    {
        public required int LeaveId { get; set; }
        public required int TotalLeaves { get; set; }
        public required int ForYear { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        //Navigation Property
        #region Crew Member to LeaveInformation(one-to-one)
        /* Crew Member to LeaveInformation(one-to-one)
         * A Single crew menber can have only one record of Total Leaves.
         * Each leave record is associated with single Crew Member
         * This is classic one-to-one relationship.
         */
        [ForeignKey("CrewId")]
        public int CrewId { get; set; }
        public virtual CrewInformation? crewInfo { get; set; }
        #endregion


        public ICollection<LeaveSummary> LeaveSummaries { get; set; } = new List<LeaveSummary>();

        public LeaveInformation()
        {

        }
    }
}
