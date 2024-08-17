using DecortetServer.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Persistense.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.ClientName)
                .IsRequired();
            builder.Property(b => b.Phone)
                .IsRequired();
            builder.Property(b => b.Address)
                .IsRequired();
            builder.Property(b => b.Town)
                .IsRequired();
            builder.Property(b => b.Region)
                .IsRequired();
        }
    }
}
