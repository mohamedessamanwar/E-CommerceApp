using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data.Context
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configure your database connection string here
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=E-CommerceDB;Integrated Security=True;TrustServerCertificate=True");
            }
        }
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceContext).Assembly);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
