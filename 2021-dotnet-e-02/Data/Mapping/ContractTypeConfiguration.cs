using System;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class ContractTypeConfiguration : IEntityTypeConfiguration<ActemiumContractType>
    {
        public void Configure(EntityTypeBuilder<ActemiumContractType> builder)
        {
            builder.ToTable("ACTEMIUMCONTRACTTYPE");

            builder.HasKey(t => t.ContractTypeId);
            
            builder.Property(t => t.ContractTypeId).HasColumnName("CONTRACTTYPEID");

            builder.Property(t => t.Name).HasColumnName("NAME");
            builder.Property(t => t.Status).HasColumnName("STATUS")
                .HasConversion(v => v.ToString(),
                    v => (ContractTypeStatus)Enum.Parse(typeof(ContractTypeStatus), v));
            builder.Property(t => t.HasEmail).HasColumnName("HASEMAIL");
            builder.Property(t => t.HasPhone).HasColumnName("HASPHONE");
            builder.Property(t => t.HasApplication).HasColumnName("HASAPPLICATION");

            builder.Property(t => t.TimeStamp).HasColumnName("TIMESTAMP")
                .HasConversion(v => v.ToString(),
                    v => (Timestamp)Enum.Parse(typeof(Timestamp), v));

            builder.Property(t => t.MaxHandlingTime).HasColumnName("MAXHANDLINGTIME");
            builder.Property(t => t.MinThroughputTime).HasColumnName("MINTHROUGHPUTTIME");
            builder.Property(t => t.Price).HasColumnName("PRICE");

            
        }
    }
}