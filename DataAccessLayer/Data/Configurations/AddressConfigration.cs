using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configurations
{
    public class AddressConfigration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a=> a.Description).IsRequired().HasMaxLength(255);
            builder.Property(a => a.City).IsRequired().HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(a => a.State).IsRequired().HasDefaultValue("Available");
        }
    }
}
