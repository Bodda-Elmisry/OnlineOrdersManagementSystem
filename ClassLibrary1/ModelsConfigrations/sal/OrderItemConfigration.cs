using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineOrderManagementSystem.Domain.Enums;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.ModelsConfigrations.sal
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", schema: "sal");

            builder.HasKey(oi => oi.Id);

            builder.HasIndex(builder => new { builder.OrderId, builder.ProductId })
                .IsUnique();

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems) // Indicate the navigation property in Order
                .HasForeignKey(oi => oi.OrderId) // Explicitly specify the foreign key
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.Subtotal)
                .HasPrecision((int)PropsLengthEnum.pricePercision, (int)PropsLengthEnum.priceScale)
                .IsRequired();

        }
    }
}
