using System;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<ActemiumEmployee>
    {
        public void Configure(EntityTypeBuilder<ActemiumEmployee> builder)
        {
            builder.ToTable("ACTEMIUMEMPLOYEE");

            //TODO
            builder.Ignore(t => t.Specialties);
            builder.Property(t => t.Address);
            builder.Property(t => t.PhoneNumber);
            builder.Property(t => t.Email).HasColumnName("EMAILADDRESS");
            builder.Property(t => t.Role)
                .HasConversion(v => v.ToString(),
                    v => (EmployeeRole)Enum.Parse(typeof(EmployeeRole), v));
        }
    }
}