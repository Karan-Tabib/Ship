using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class LeaveSummaryConfig :IEntityTypeConfiguration<LeaveSummary>
    {
        public void Configure(EntityTypeBuilder<LeaveSummary> builder)
        {
            builder.ToTable("LeaveSummary");
            builder.HasKey(x => x.LeaveSummaryId);
            builder.Property(x => x.LeaveSummaryId).UseIdentityColumn();
            builder.Property(x => x.NoOfDaysOff).HasMaxLength(3);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.HasOne(x =>x.LeavesInforms).WithMany(c =>c.LeaveSummaries).HasForeignKey(c => c.LeaveId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
