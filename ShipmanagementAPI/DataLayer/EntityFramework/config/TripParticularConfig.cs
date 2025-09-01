using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class TripParticularConfig : IEntityTypeConfiguration<TripParticular>
    {
        public void Configure(EntityTypeBuilder<TripParticular> builder)
        {
            builder.HasKey(tp => tp.TripParticularId);
            builder.Property(tp => tp.TripParticularId).UseIdentityColumn();
            
            // Configure the one-to-many relationship between TripParticular and TripInfo
            builder.HasOne(tp => tp.TripInfo)
                   .WithMany(ti => ti.TripParticulars)
                   .HasForeignKey(tp => tp.TripId)
                   .OnDelete(DeleteBehavior.Cascade);  // Optional: delete TripParticulars when TripInfo is deleted

            // Configure properties
            builder.Property(tp => tp.FishId)
                   .IsRequired()
                   .HasMaxLength(100);  // Limit FishName to 100 characters

            builder.Property(tp => tp.RatePerKg)
                   .HasColumnType("decimal(18, 2)");  // Configure RatePerKg as decimal

            builder.Property(tp => tp.TotalWeight)
                   .HasColumnType("decimal(18, 2)");  // Configure TotalWeight as decimal

            builder.Property(tp => tp.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)");  // Configure Amount as required decimal

            builder.Property(tp => tp.TripDate)
                   .IsRequired();
        }
    }
}
