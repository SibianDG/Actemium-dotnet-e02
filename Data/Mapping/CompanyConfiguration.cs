using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class CompanyConfiguration : IEntityTypeConfiguration<ActemiumCompany>
    {
        public void Configure(EntityTypeBuilder<ActemiumCompany> builder)
        {
            builder.ToTable("ACTEMIUMCOMPANY");

            builder.HasKey(t => t.CompanyId);
            
            builder.Property(t => t.Name);
            builder.Property(t => t.Country);
            builder.Property(t => t.City);
            builder.Property(t => t.Address);
            builder.Property(t => t.PhoneNumber);
            builder.Property(t => t.RegistrationDate);
        }
    }
}