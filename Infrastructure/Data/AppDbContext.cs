using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Product => Set<Product>();
        public DbSet<Pallet> Pallet => Set<Pallet>();
        public DbSet<Inventory> Inventory => Set<Inventory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Sku)
                .IsUnique();

            modelBuilder.Entity<Inventory>()
                .HasIndex(i => new { i.PalletId, i.PositionNumber })
                .IsUnique();

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Inventory)
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Pallet)
                .WithMany(p => p.Inventory)
                .HasForeignKey(i => i.PalletId);

            base.OnModelCreating(modelBuilder);
        }
    }
}