using System;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
    {
        public void Configure(EntityTypeBuilder<LoginAttempt> builder)
        {
            builder.ToTable("LOGINATTEMPT");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("ID");
            builder.Property(t => t.DateAndTime)
                .HasColumnName("DATEANDTIME");
            builder.Property(t => t.Username)
                .HasColumnName("USERNAME");
            builder.Property(t => t.LoginStatus)
                .HasColumnName("LOGINSTATUS")
                .HasConversion(v => v.ToString(),
                    v => (LoginStatus)Enum.Parse(typeof(LoginStatus), v));

            builder.Property<int>("USERMODEL_USERID");
            builder.HasOne(t => t.UserModel).WithMany(t => t.LoginAttempts).HasForeignKey("USERMODEL_USERID");
            
        }
        
    }
}