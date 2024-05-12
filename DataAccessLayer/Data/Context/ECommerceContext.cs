using DataAccessLayer.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data.Context
{
    public class ECommerceContext : IdentityDbContext<ApplicationUser>
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
            modelBuilder.Entity<ProductWithCategory>(p => { p.HasNoKey().ToView(null); });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductWithCategory> ProductWithCategories { get; set; }
    }
}
