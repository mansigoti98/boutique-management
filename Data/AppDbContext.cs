using BoutiqueManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BoutiqueItem> BoutiqueItems { get; set; }
        public DbSet<RentalTransaction> RentalTransactions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoutiqueItem>()
                .HasIndex(b => b.ItemCode)
                .IsUnique();

            modelBuilder.Entity<RentalTransaction>()
    .HasOne(rt => rt.BoutiqueItem)
    .WithMany()
    .HasPrincipalKey(b => b.ItemCode) 
    .HasForeignKey(rt => rt.ItemCode);
        }

    }
}