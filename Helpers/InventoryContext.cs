using AGLS.Models;
using Microsoft.EntityFrameworkCore;

namespace agsl_test.Helpers
{
    public class InventoryContext : DbContext
    {
        public DbSet<InventoryItem> InventoryItems { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder mb) {
            mb.Entity<InventoryItem>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Barcode).IsRequired().HasMaxLength(13);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(128);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(5,2)");
            });
        }
    }
}
