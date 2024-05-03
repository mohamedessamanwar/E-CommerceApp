using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccessLayer.Data.Configurations
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.Property(p => p.CreateOn).HasDefaultValueSql("GETDATE()");
            // modelBuilder.Property(p => p.UpdateOn).HasDefaultValueSql("GETDATE()");
            modelBuilder.Property(p => p.Name).HasMaxLength(255);


            #region Category Seeding
            List<Category> categoryList = new List<Category>
        {
            new Category{Id=1,Name="Apple"},
            new Category{Id=2,Name="Dell"},
            new Category{Id=3,Name="HP"},
            new Category{Id=4,Name="Lenovo"},
            new Category{Id=5,Name="ASUS"},
            new Category{Id=6,Name="Acer"},
            new Category{Id=7,Name="Microsoft"},
            new Category{Id=8,Name="MSI"},
            new Category{Id=9,Name="Razer"},
            new Category{Id=10,Name="Samsung"},
        };
            modelBuilder.HasData(categoryList);
            #endregion
        }
    }
}
