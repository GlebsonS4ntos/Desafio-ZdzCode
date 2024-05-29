using Desafio.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Data
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.PartentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
