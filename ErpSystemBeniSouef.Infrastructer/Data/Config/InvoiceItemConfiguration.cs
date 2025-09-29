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
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(ii => !ii.IsDeleted);
        }
    }
}
