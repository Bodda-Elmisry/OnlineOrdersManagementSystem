using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.ModelsConfigrations.sal
{
    public class OrderStatusHistoryConfigration : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.ToTable("OrderStatusHistory", schema: "sal");

            builder.HasKey(x => x.Id);

            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();
        }
    }
}
