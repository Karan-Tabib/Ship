using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class GoodsDetails
    {
        public string FishName { get; set; }
        public decimal? RatePerKg { get; set; } = 0;
        public decimal? TotalWeight { get; set; } = 0;
        public decimal Amount { get; set; }

        public GoodsDetails(string fishname, decimal rateperkg, decimal totalweight, decimal amount)
        {
            this.FishName = fishname;
            this.RatePerKg = rateperkg;
            this.TotalWeight = totalweight;
            if (this.RatePerKg != 0 && totalweight != 0)
            {
                this.Amount = (decimal)(this.RatePerKg * this.TotalWeight);
            }
            else
            {
                this.Amount = amount;
            }
        }
    }
    public class TripParticular
    {
        public required int TripParticularId { get; set; }
        public required DateTime TripDate { get; set; }
        //public List<GoodsDetails>? TripParticulars { set; get; }

        // Properties previously in GoodsDetails
        public int FishId { get; set; }       // Name of the fish
        public decimal RatePerKg { get; set; } = 0;  // Rate per kilogram
        public decimal TotalWeight { get; set; } = 0;  // Total weight of goods
        public decimal Amount { get; set; }        // Amount calculated based on weight and rate
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdatedDate { get; set; }
        /*
         * TripInfo to TripParticular (One-to-Many):
         *      Each TripInfo can have multiple TripParticular records.
         *      Each TripParticular is associated with one TripInfo.
         *      This is also a one-to-many relationship.
         */
        // Foreign key for TripInfo
        [ForeignKey("TripId")]
        public int TripId { get; set; }
        public TripInformation TripInfo { get; set; }

    }
}
