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
    public class SupplierAccountConfiguration : IEntityTypeConfiguration<SupplierAccount>
    {
        public void Configure(EntityTypeBuilder<SupplierAccount> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.Amount)
                .HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(sa => !sa.IsDeleted);
        }
    }
}
