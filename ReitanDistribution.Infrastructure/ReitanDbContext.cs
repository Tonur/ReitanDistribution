using Microsoft.EntityFrameworkCore;
using ReitanDistribution.Core;

namespace ReitanDistribution.Infrastructure
{
    public class ReitanDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO When this gets out of testing, use a SQL database instead
            optionsBuilder.UseInMemoryDatabase("Reitan");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
