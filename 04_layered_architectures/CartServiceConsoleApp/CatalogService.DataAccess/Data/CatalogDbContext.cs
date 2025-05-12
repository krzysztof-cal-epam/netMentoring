using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Outbox> Outbox { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.ImageUrl)
                    .HasConversion(
                      v => v == null ? null : v.ToString(),
                      v => v == null ? null : new Uri(v));

                entity.HasOne(c => c.ParentCategory)
                    .WithMany()
                    .HasForeignKey(c => c.ParentCategoryId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.Description)
                    .IsRequired(false);

                entity.Property(e => e.Price).IsRequired();

                entity.Property(e => e.Amount).IsRequired();

                entity.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Outbox>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EventType).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Payload).IsRequired();
                entity.Property(e => e.IsProcessed).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
