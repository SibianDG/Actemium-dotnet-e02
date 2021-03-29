using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("USERMODEL");
            
            builder.HasDiscriminator<string>("DTYPE")
                .HasValue<ActemiumCustomer>("ActemiumCustomer")
                .HasValue<ActemiumEmployee>("ActemiumEmployee");

            builder.HasKey(t => t.UserId);
            builder.Property(t => t.UserName);
            builder.Property(t => t.Password);
            builder.Property(t => t.FirstName);
            builder.Property(t => t.LastName);
            builder.Property(t => t.Status);
            //builder.Property(t => t.RegistrationDate);
            builder.Property(t => t.FailedLoginAttempts);
            
            
            builder.HasMany(t => t.LoginAttempts).WithOne();
        }
        
    }
}