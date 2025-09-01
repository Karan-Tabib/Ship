using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class FishInformationConfig : IEntityTypeConfiguration<FishInformation>
    {
        public void Configure(EntityTypeBuilder<FishInformation> builder)
        {
            builder.ToTable("FishInformation");
            builder.HasKey(x => x.FishId);
            builder.Property(x => x.FishId).UseIdentityColumn();
            builder.Property(x => x.FishName).IsRequired().HasMaxLength(250);

            builder.HasData(new List<FishInformation>()
            {
               new FishInformation(){FishId=1,FishName ="Promplet"},
               new FishInformation(){FishId=2,FishName ="Halwa"},
               new FishInformation(){FishId=3,FishName ="Surmai"},
               new FishInformation(){FishId=4,FishName ="Tuna"},
               new FishInformation(){FishId=5,FishName ="Pronze"},
               new FishInformation(){FishId=6,FishName ="Ribben"},
               new FishInformation(){FishId=7,FishName ="White"},
               new FishInformation(){FishId=8,FishName ="Lobster"},
               new FishInformation(){FishId=9,FishName ="Bangada"},
               new FishInformation(){FishId=10,FishName ="Bombil"},

            });
        }
    }
}
