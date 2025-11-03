using Microsoft.EntityFrameworkCore;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Data
{
    public class FactoryDbContext : DbContext
    {
        public FactoryDbContext(DbContextOptions<FactoryDbContext> options)
            : base(options)
        {
        }

        // Entity DbSets
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MonthlyPay> MonthlyPays { get; set; }
        public DbSet<DailyWorkSchedule> DailyWorkSchedules { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductionRoom> ProductionRooms { get; set; }
        public DbSet<ProductionPipelineLine> ProductionPipelineLines { get; set; }

        // Junction Tables
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
        public DbSet<EmployeeProductionLine> EmployeeProductionLines { get; set; }

        // Authentication
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here
            // TODO: Add your custom configurations when database is ready

            // Example: Configure composite keys, indexes, etc.
            // modelBuilder.Entity<ProductMaterial>()
            //     .HasIndex(pm => new { pm.ProductId, pm.MaterialId });

            // Configure decimal precision for monetary values
            modelBuilder.Entity<MonthlyPay>()
                .Property(mp => mp.BaseSalary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MonthlyPay>()
                .Property(mp => mp.Bonus)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MonthlyPay>()
                .Property(mp => mp.Deductions)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MonthlyPay>()
                .Property(mp => mp.AdditionalIncome)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MonthlyPay>()
                .Property(mp => mp.TotalPay)
                .HasPrecision(18, 2);
        }
    }
}
