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

            builder.Property(t => t.CompanyId)
                .HasColumnName("COMPANYID");
            builder.Property(t => t.Name)
                .HasColumnName("NAME");
            builder.Property(t => t.Country)
                .HasColumnName("COUNTRY");
            builder.Property(t => t.City)
                .HasColumnName("CITY");
            builder.Property(t => t.Address)
                .HasColumnName("ADDRESS");
            builder.Property(t => t.PhoneNumber)
                .HasColumnName("PHONENUMBER");
            builder.Property(t => t.RegistrationDate)
                .HasColumnName("REGISTRATIONDATE");

            //builder.HasMany(t => t.Tickets).WithOne();

            builder.HasMany(t => t.Contracts).WithOne(t => t.Company)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(t => t.ContractId);
        }
    }
}