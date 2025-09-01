using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class LeaveInformationConfig : IEntityTypeConfiguration<LeaveInformation>
    {
        public void Configure(EntityTypeBuilder<LeaveInformation> builder)
        {
            builder.ToTable("LeaveInformation");
            builder.HasKey(x => x.LeaveId);
            builder.Property(x => x.LeaveId).UseIdentityColumn();
            builder.Property(x => x.TotalLeaves).IsRequired().HasMaxLength(3);
            builder.Property(x =>x.ForYear).IsRequired().HasMaxLength(4);

        }
    }
}
