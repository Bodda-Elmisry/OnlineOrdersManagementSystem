using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineOrderManagementSystem.Domain.Enums;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.ModelsConfigrations.Cust
{
    public class CustomerConfigration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", schema: "cust");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength((int)PropsLengthEnum.name)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength((int)PropsLengthEnum.email)
                .IsRequired(false);

            builder.Property(c => c.Address)
                .HasMaxLength((int)PropsLengthEnum.address)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength((int)PropsLengthEnum.phoneNumber)
                .IsRequired();
        }
    }
}
