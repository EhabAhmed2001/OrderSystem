using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(oi=> oi.Product)
                   .WithOne()
                   .HasForeignKey<OrderItem>(oi => oi.ProductId);

            builder.Property(oi=>oi.Discount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.UnitPrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
