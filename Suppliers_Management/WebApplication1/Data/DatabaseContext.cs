using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts):base(opts)
        {
            
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<SupplierCategory> SupplierCategories { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.Country)
                .WithMany(c => c.Suppliers)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.Category)
                .WithMany(sc => sc.Suppliers)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }*/
    }
}
