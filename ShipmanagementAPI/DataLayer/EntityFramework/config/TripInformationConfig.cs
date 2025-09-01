using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class TripInformationConfig : IEntityTypeConfiguration<TripInformation>
    {
        public void Configure(EntityTypeBuilder<TripInformation> builder)
        {
            builder.ToTable("TripInformation");
            builder.HasKey(x => x.TripId);
            builder.Property(x => x.TripId).UseIdentityColumn();
            builder.Property(x => x.TripDescription).IsRequired().HasMaxLength(500);


            builder.HasMany(t => t.TripParticulars)
              .WithOne(tp => tp.TripInfo)
              .HasForeignKey(tp => tp.TripId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TripExpenditures).WithOne(x => x.TripInfo).HasForeignKey(x => x.TripId).OnDelete(DeleteBehavior.Cascade);


            builder.HasData(new List<TripInformation>
            {
                new TripInformation(){TripId =1,TripStartDate = DateTime.Now,TripEndDate = DateTime.Now.AddDays(7),TripName="First Trip",TripDescription="First Trip",BoatId=1,CreatedDate =DateTime.Now,UpdatedDate=DateTime.Now},
                new TripInformation(){TripId =2,TripStartDate = DateTime.Now,TripEndDate = DateTime.Now.AddDays(7),TripName="Second Trip",TripDescription="Second Trip", BoatId = 1,CreatedDate =DateTime.Now,UpdatedDate=DateTime.Now},
                new TripInformation(){TripId =3,TripStartDate = DateTime.Now,TripEndDate = DateTime.Now.AddDays(7),TripName="Third Trip",TripDescription="Third Trip", BoatId = 1,CreatedDate =DateTime.Now,UpdatedDate=DateTime.Now},
            });
        }
    }
}
