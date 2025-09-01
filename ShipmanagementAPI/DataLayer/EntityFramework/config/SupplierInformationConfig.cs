using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;

namespace DataLayer.EntityFramework.config
{
    public class SupplierInformationConfig : IEntityTypeConfiguration<SupplierInformation>
    {
        public void Configure(EntityTypeBuilder<SupplierInformation> builder)
        {
            builder.ToTable("SupplierInformation");
            builder.HasKey(x => x.SupplierId);
            builder.Property(x => x.SupplierId).UseIdentityColumn();
            builder.Property(prop => prop.Firstname).IsRequired().HasMaxLength(250);
            builder.Property(prop => prop.Middlename).IsRequired().HasMaxLength(250);
            builder.Property(prop => prop.Lastname).IsRequired().HasMaxLength(250);
            builder.Property(prop => prop.Email).IsRequired().HasMaxLength(250);
            builder.Property(prop => prop.Address).IsRequired().HasMaxLength(500);
            builder.Property(prop => prop.Phone).IsRequired().HasMaxLength(10);

            builder.HasData(new List<SupplierInformation>
            {
                new SupplierInformation(){SupplierId=1,Firstname="ABC",Middlename="PQR",Lastname="XYZ",Email="abczyx@gmail.com",Address="Harnai",Phone="8478523693"},
            }
            );
        }
    }
}
