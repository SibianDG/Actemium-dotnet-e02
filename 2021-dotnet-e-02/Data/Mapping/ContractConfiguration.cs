using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class ContractConfiguration : IEntityTypeConfiguration<ActemiumContract>
    {
        public void Configure(EntityTypeBuilder<ActemiumContract> builder)
        {
            builder.ToTable("ACTEMIUMCONTRACT");

            builder.HasKey(t => t.ContractId).HasName("CONTRACTID");
            
            builder.Property(t => t.StartDate)
                .HasColumnName("STARTDATE");
            builder.Property(t => t.EndDate)
                .HasColumnName("ENDDATE");
            builder.Property(t => t.Status)
                .HasColumnName("STATUS");

            builder.HasOne(t => t.ContractType).WithMany().IsRequired().HasForeignKey(t => t.ContractId);

        }
    }
}
