using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.Models
{
    public class LeaveInformation
    {
        public required int LeaveId { get; set; }
        public required int TotalAvailableLeaves { get; set; }
        public required int Currentyear {  get; set; }

        //Navigation Property
        #region Crew Member to LeaveInformation(one-to-one)
        /* Crew Member to LeaveInformation(one-to-one)
         * A Single crew menber can have only one record of Total Leaves.
         * Each leave record is associated with single Crew Member
         * This is classic one-to-one relationship.
         */
        [ForeignKey("CrewId")]
        public int CrewId { get;set; }
        public  virtual CrewInformation? crewInfo { get; set; }
        #endregion
        
        public LeaveInformation()
        {
            
        }
    }
}
