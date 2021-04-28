using System;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("USERMODEL");

            /*builder.HasDiscriminator<string>("DTYPE")
                .HasValue<ActemiumCustomer>("ActemiumCustomer")
                .HasValue<ActemiumEmployee>("ActemiumEmployee");*/

            builder.HasKey(t => t.UserId);
            builder.Property(t => t.UserName);
            builder.Property(t => t.Password);
            builder.Property(t => t.FirstName);
            builder.Property(t => t.LastName);
            builder.Property(t => t.Status)
                .HasConversion(v => v.ToString(),
                    v => (UserStatus)Enum.Parse(typeof(UserStatus), v));
            builder.Property(t => t.RegistrationDate).HasColumnName("REGISTRATIONDATE");
            builder.Property(t => t.FailedLoginAttempts);

            //builder.HasMany(t => t.Comments).WithOne(t=> t.UserModel)
            
            builder.HasMany(t => t.LoginAttempts).WithOne(t=> t.UserModel).HasForeignKey(t => t.Id);

            //builder.Property<int>("technicians_USERID");
            //builder.HasMany(t => t.TicketTechnicians).WithOne(t => t.Technician).HasForeignKey(t => t.UserId);
            //builder.HasMany(t => t.TicketTechnicians).WithOne(t => t.Technician).HasForeignKey("technicians_USERID");
        }
        
    }
}
