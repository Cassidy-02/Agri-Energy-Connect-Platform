using Agri_Energy_Connect_Platform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agri_Energy_Connect_Platform.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Fix for CS0029 and CS1662
            // Correct the navigation property and foreign key relationship
            builder.Entity<ApplicationUser>()
                .HasOne(f => f.Farmer)
                .WithMany() 
                .HasForeignKey(p => p.FarmerId) // Use `FarmerId` as the foreign key.
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}