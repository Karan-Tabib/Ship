using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Entities;
using System.Reflection.Emit;


namespace DataLayer.EntityFramework.config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("UserDefinition");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).UseIdentityColumn();
            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Middlename).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);

            //navigation property using fluent API
            builder.HasMany(user => user.Boats).WithOne(boat => boat.UserInfo).HasForeignKey(boat => boat.UserId);
            //User.HasMany(boat).Withone(user).hasForeignkey(id)

            builder.HasData(new List<User>()
            {
                new User(){UserId =1,Firstname= "Karan",Middlename="Bhagwan",Lastname ="Tabib",Email="Karan@gmail.com",Address="Pune",Phone="123456789",Password="1234"},
                new User(){UserId =2,Firstname= "Sagar",Middlename="Bhagwan",Lastname ="Tabib",Email="Sagar@gmail.com",Address="Pune",Phone="123456789",Password="4321"},
                new User(){UserId =3,Firstname= "Arvind",Middlename="Bhagwan",Lastname ="Tabib",Email="Arvind@gmail.com",Address="Pune",Phone="123456789",Password="7895"},
            });
        }
    }
}
