using DataLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository;
using BusinessLayer.BusinessBL;
using BusinessLayer.Abstraction;

namespace BusinessLayer
{

    public static class ServicesConfiguration
    {
        public static void RegisterServices(this IServiceCollection Services, IConfiguration config)
        {
            Console.WriteLine(config.GetConnectionString("MyConnectionSting"));
            Services.AddDbContextPool<ShipManagemntDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("MyConnectionSting"));
            });

            // Register generic repository
            Services.AddScoped(typeof(ICrewRepository), typeof(CrewRepository));
            Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            Services.AddScoped(typeof(IBoatRepository), typeof(BoatRepository));
            Services.AddScoped(typeof(IShipRepository<>), typeof(ShipRepository<>));
            Services.AddScoped(typeof(ISalaryRepository), typeof(SalaryRepository));
            Services.AddScoped(typeof(ISalarySummaryRepository), typeof(SalarySummaryRepository));
            Services.AddScoped(typeof(ILeaveRepository), typeof(LeaveRepository));
            Services.AddScoped(typeof(ILeaveSummaryRepository), typeof(LeaveSummaryRepository));
            Services.AddScoped(typeof(IFishRepository), typeof(FishRepository));
            Services.AddScoped(typeof(ITripRepository), typeof(TripRepository));

            Services.AddScoped(typeof(IShipBL<>), typeof(ShipBL<>));
            Services.AddScoped(typeof(IUserBL), typeof(UserBL));
            Services.AddScoped(typeof(IBoatBL), typeof(BoatBL));
            Services.AddScoped(typeof(ICrewBL), typeof(CrewBL));
            Services.AddScoped(typeof(ISalaryBL), typeof(SalaryBl));
            Services.AddScoped(typeof(ISalarySummaryBL), typeof(SalarySummaryBl));
            Services.AddScoped(typeof(ILeavesBL), typeof(LeavesBL));
            Services.AddScoped(typeof(ILeaveSummaryBL), typeof(LeavesSummaryBl));
            Services.AddScoped(typeof(IFishBL), typeof(FishBL));
            Services.AddScoped(typeof(ITripBL), typeof(TripBL));
            // Add DbContext dependency
            //Services.AddDbContext<ShipManagemntDbContext>(options =>
            //{
            //    options.UseSqlServer(config.GetConnectionString("MyConnectionString"));
            //}
            //);


            // Configure authentication
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = config["Jwt:Issuer"],
            //        ValidAudience = config["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
            //    };
            //});
        }
    }

}
