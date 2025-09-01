using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class TripExpenditureConfig :IEntityTypeConfiguration<TripExpenditure>
    {
        public void Configure(EntityTypeBuilder<TripExpenditure> builder)
        {
            // Define primary key
            builder.HasKey(te => te.TripExpenditureId);
            builder.Property(x => x.TripExpenditureId).UseIdentityColumn();

            // Define the relationship between TripExpenditure and TripInfo
            builder.HasOne(te => te.TripInfo)
                   .WithMany(ti => ti.TripExpenditures)  // A TripInfo can have multiple expenditures
                   .HasForeignKey(te => te.TripId)       // Foreign key for TripInfo
                   .OnDelete(DeleteBehavior.Cascade);    // Optional: delete expenditures if TripInfo is deleted

            // Configure properties
            builder.Property(te => te.Reason)
                   .IsRequired()
                   .HasMaxLength(200);  // Limit the length of the Reason field

            builder.Property(te => te.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)");  // Configure Amount as decimal

            builder.Property(te => te.TripDate)
                   .IsRequired();  // TripDate is required
        }
    }
}
