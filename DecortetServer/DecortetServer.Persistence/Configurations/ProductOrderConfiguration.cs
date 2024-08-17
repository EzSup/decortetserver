using DecortetServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Persistense.Configurations
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(b => new { b.OrderId , b.ProductId});

            builder.Property(b => b.Quantity)
                .IsRequired();

            builder.HasOne(p => p.Product)
                .WithMany(po => po.ProductOrders)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Order)
                .WithMany(po => po.ProductOrders)
                .HasForeignKey(po => po.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
