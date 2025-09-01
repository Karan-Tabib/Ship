using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class SalarySummaryConfig : IEntityTypeConfiguration<SalarySummary>
    {
        public void Configure(EntityTypeBuilder<SalarySummary> builder)
        {
            builder.ToTable("SalarySummary");
            builder.HasKey(x => x.SalarySummaryId);
            builder.Property(x => x.SalarySummaryId).UseIdentityColumn();
            builder.Property(x => x.AmountTaken).HasPrecision(18,2);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.HasOne(x =>x.Salaries).WithMany().HasForeignKey(x =>x.SalaryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
