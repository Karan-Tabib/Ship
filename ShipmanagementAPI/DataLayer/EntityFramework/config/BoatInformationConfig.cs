using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class BoatInformationConfig : IEntityTypeConfiguration<BoatInformation>
    {
        public void Configure(EntityTypeBuilder<BoatInformation> builder)
        {
            builder.ToTable("BoatInformation");
            builder.HasKey(x => x.BoatId);
            builder.Property(x => x.BoatId).UseIdentityColumn();
            builder.Property(x => x.BoatName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.BoatType).IsRequired().HasMaxLength(50);

            //Navigation propety
            // boat has many crew members . each crew belong to one boat with boat id as for
            builder.HasMany(boat => boat.CrewMembers).WithOne(creinfo => creinfo.Boat).HasForeignKey(creinfo => creinfo.BoatId);
            builder.HasMany(boat => boat.TripInfos).WithOne(trips => trips.Boat).HasForeignKey(trips => trips.BoatId);

            builder.HasData(new List<BoatInformation>()
            {
                new BoatInformation(){BoatId=1,BoatName="Bhagwati",BoatType ="Daldi",UserId =3},
                new BoatInformation(){BoatId=2,BoatName="Dhanlaxmi",BoatType ="Trawler",UserId=3},
                new BoatInformation(){BoatId=3,BoatName="dwarkashish",BoatType ="Trawler",UserId=1}

            });

        }
    }
}
