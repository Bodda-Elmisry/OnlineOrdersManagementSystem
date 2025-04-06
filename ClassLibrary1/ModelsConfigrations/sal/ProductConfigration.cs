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
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "sal");

            builder.HasKey(p => p.Id);

            builder.Property(p=> p.Name)
                .HasMaxLength((int)PropsLengthEnum.name)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength((int)PropsLengthEnum.description)
                .IsRequired(false);

            builder.Property(p => p.Price)
                .HasPrecision((int)PropsLengthEnum.pricePercision, (int)PropsLengthEnum.priceScale)
                .IsRequired();

            builder.Property(p => p.StockQuantity)
                .IsRequired();


        }
    }
}
