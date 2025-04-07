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

            builder.HasKey(oi => new { oi.OrderId, oi.ProductId });

            builder.HasOne(oi=> oi.Order)
                .WithMany()
                .HasForeignKey(oi => oi.OrderId)
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
