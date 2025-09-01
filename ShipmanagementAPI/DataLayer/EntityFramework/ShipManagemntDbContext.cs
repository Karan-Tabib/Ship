using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using DataLayer.EntityFramework.config;
namespace DataLayer.EntityFramework
{
    public class ShipManagemntDbContext : DbContext
    {
        public ShipManagemntDbContext(DbContextOptions<ShipManagemntDbContext> options) : 
            base(options)
        {
            
        }

        public DbSet<User> UserDefinition { get; set; }
        public DbSet<BoatInformation> BoatInformation { get; set; }
        public DbSet<CrewInformation> CrewInformation { get; set; }
        public DbSet<FishInformation> FishInformation { get; set; }
        public DbSet<SalaryInformation> CrewSalaryInformation { get; set; }
        public DbSet<SupplierInformation> SupplierInformation { get; set; }
        public DbSet<TripInformation> TripInformation { get; set; }
        public DbSet<LeaveInformation> LeaveInformation { get; set; }
        public DbSet<SalarySummary> CrewSalarySummary { get; set; }
        public DbSet<LeaveSummary> CrewLeaveSummary { get; set; }
        public DbSet<TripExpenditure> TripExpenditures { get; set; }
        public DbSet<TripParticular> TripParticulars { get; set; }
         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new BoatInformationConfig());
            modelBuilder.ApplyConfiguration(new CrewInformationConfig());
            modelBuilder.ApplyConfiguration(new FishInformationConfig());
            modelBuilder.ApplyConfiguration(new LeaveInformationConfig());
            modelBuilder.ApplyConfiguration(new SupplierInformationConfig());
            modelBuilder.ApplyConfiguration(new LeaveSummaryConfig());
            modelBuilder.ApplyConfiguration(new SalarySummaryConfig());
            modelBuilder.ApplyConfiguration(new SalaryInformationConfig());
            modelBuilder.ApplyConfiguration(new TripExpenditureConfig());
            modelBuilder.ApplyConfiguration(new TripParticularConfig());
            modelBuilder.ApplyConfiguration(new TripInformationConfig());


            base.OnModelCreating(modelBuilder);
        }
    }
}
