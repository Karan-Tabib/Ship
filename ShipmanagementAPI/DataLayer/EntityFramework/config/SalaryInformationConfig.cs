using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class SalaryInformationConfig : IEntityTypeConfiguration<SalaryInformation>
    {
        public void Configure(EntityTypeBuilder<SalaryInformation> builder)
        {
            builder.ToTable("SalaryInformation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.TotalSalary).HasPrecision(18, 2).IsRequired();
            builder.Property(x =>x.ForYear).IsRequired().HasMaxLength(4);

            builder.HasData(new List<SalaryInformation>()
            {
                new SalaryInformation(){Id=1,CrewId =1,startDate= DateTime.Now,endDate=DateTime.Now.AddYears(1),TotalSalary =200000,ForYear=2024},
                new SalaryInformation(){Id=2,CrewId =2,startDate= DateTime.Now,endDate=DateTime.Now.AddYears(1),TotalSalary =220000,ForYear=2024},
                new SalaryInformation(){Id=3,CrewId =3,startDate= DateTime.Now,endDate=DateTime.Now.AddYears(1),TotalSalary =240000,ForYear=2024},
            });
        }
    }

}
