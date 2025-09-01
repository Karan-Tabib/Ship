using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;
using System.Net;

namespace DataLayer.EntityFramework.config
{
    public class CrewInformationConfig : IEntityTypeConfiguration<CrewInformation>
    {
        public void Configure(EntityTypeBuilder<CrewInformation> builder)
        {
            builder.ToTable("CrewInformation");
            builder.HasKey(x => x.CrewID);
            builder.Property(x => x.CrewID).UseIdentityColumn();
            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Middlename).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(250);
            builder.Property(x => x.AdharNo).IsRequired().HasMaxLength(16);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);

            //relationships
            builder.HasOne(crewMember => crewMember.LeaveInfo).WithOne(leaveinfo => leaveinfo.crewInfo).HasForeignKey<LeaveInformation>(leaveinfo => leaveinfo.CrewId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(crewMember => crewMember.SalaryInfo).WithOne(salaryinfo => salaryinfo.CrewInfos).HasForeignKey<SalaryInformation>(salaryinfo => salaryinfo.CrewId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new List<CrewInformation>()
            {
                new CrewInformation(){CrewID =1,Firstname="Harishchandra", Middlename="rama",Lastname="Pawase",AdharNo="1234567899876543",Address="Paj",Phone="9082716352",BoatId=1},
                new CrewInformation(){CrewID =2,Firstname="Manoj", Middlename="shankar",Lastname="Choagle",AdharNo="1334567899876543",Address="Paj",Phone="9082716353",BoatId=1},
                new CrewInformation(){CrewID =3,Firstname="Vaman", Middlename="shiva",Lastname="Pawase",AdharNo="1434567899876543",Address="Paj",Phone="9082716354", BoatId = 1},
                new CrewInformation(){CrewID =4,Firstname="Kashinath", Middlename="Maya",Lastname="Tabib",AdharNo="1534567899876543",Address="Paj",Phone="9082716355", BoatId = 1},
                new CrewInformation(){CrewID =5,Firstname="Ganesh", Middlename="Eknath",Lastname="Palekar",AdharNo="1634567899876543",Address="Paj",Phone="9082716356", BoatId = 1},
                new CrewInformation(){CrewID =6,Firstname="Darshan", Middlename="Ramesh",Lastname="Tabib",AdharNo="1734567899876543",Address="Paj",Phone="9082716357", BoatId = 1},
                new CrewInformation(){CrewID =7,Firstname="Amol", Middlename="Ramesh",Lastname="Tabib",AdharNo="1834567899876543",Address="Paj",Phone="9082716358" ,BoatId=1 },
                new CrewInformation(){CrewID =8,Firstname="Navnath", Middlename="Ramesh",Lastname="Tabib",AdharNo="1934567899876543",Address="Paj",Phone="9082716359",BoatId=1},
            });
        }
    }
}
