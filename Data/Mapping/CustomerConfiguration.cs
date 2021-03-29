using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<ActemiumCustomer>
    {
        public void Configure(EntityTypeBuilder<ActemiumCustomer> builder)
        {
            builder.ToTable("ACTEMIUMCUSTOMER");

            //builder.HasOne(t => t.Company).WithMany(t => t.ContactPersons);
        }
    }
}