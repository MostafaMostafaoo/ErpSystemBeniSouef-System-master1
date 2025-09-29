using ErpSystemBeniSouef.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSystemBeniSouef.Infrastructer.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.CommissionRate)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.SalePrice)
                .HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}