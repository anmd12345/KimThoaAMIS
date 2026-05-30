using System;
using System.Reflection.Emit;
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

        // Fluent API (optional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }
    }
}

