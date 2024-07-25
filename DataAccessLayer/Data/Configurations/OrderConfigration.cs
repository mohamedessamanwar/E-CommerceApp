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
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.UpdateAt)
                .IsRequired(false);

            builder.Property(o => o.ShippingDate)
                .IsRequired(false);

            builder.Property(o => o.OrderStatus)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.OrderPaymentStatus)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.AddressId)
                .IsRequired();

            builder.Property(o => o.SessionId)
                .IsRequired(false);

            builder.Property(o => o.PaymentIntentId)
                .IsRequired(false);

            builder.Property(o => o.OrderTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            // Relationships
            builder.HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o=> o.orderDetails).WithOne(d=> d.Order).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
