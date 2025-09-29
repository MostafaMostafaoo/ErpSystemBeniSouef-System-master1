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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.DueAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
