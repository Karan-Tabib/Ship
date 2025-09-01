using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class ExpenditureDetails
    {
        public required string Reason { get; set; }
        public required decimal Amount { get; set; }

        public ExpenditureDetails()
        {

        }
        public ExpenditureDetails(string reason, decimal amt)
        {
            this.Reason = reason;
            this.Amount = amt;
        }
    }

    public class TripExpenditure
    {
        public required int TripExpenditureId { get; set; }
        public required DateTime TripDate { get; set; }
        
        // Expenditure details as properties
        public string Reason { get; set; }  // Reason for the expenditure
        public decimal Amount { get; set; }  // Amount spent
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdatedDate { get; set; }
        //public required List<ExpenditureDetails> Details { get; set; }
        //Navigation Properties

        /*
         * TripInfo to TripExpenditure (One-to-Many):
           *      Each TripInfo can have multiple TripExpenditure records.
           *      Each TripExpenditure is associated with one TripInfo.
           *      Again, this is a one-to-many relationship.
        */
        [ForeignKey("TripId")]
        public int TripId { get; set; }
        public TripInformation TripInfo { get; set; }

        public TripExpenditure()
        {

        }
    }
}
