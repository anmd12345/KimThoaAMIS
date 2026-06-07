using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
namespace ManagementKimThoa.Contexts
{
	public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<IDCard> IDCards { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<OtherInfor> OtherInfors { get; set; }

        public DbSet<HealthInsurance> HealthInsurances { get; set; }

        public DbSet<BankInfo> BankInfos { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPromotion> ProductPromotions { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Gift> Gifts { get; set; }

        public DbSet<GiftProduct> GiftProducts { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<WorkShiftAssignment> WorkShiftAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}

