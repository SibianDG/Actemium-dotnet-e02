using System;
using System.ComponentModel.DataAnnotations.Schema;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class TicketConfiguration : IEntityTypeConfiguration<ActemiumTicket>
    {
        public void Configure(EntityTypeBuilder<ActemiumTicket> builder)
        {
            builder.ToTable("ACTEMIUMTICKET");

            builder.HasKey(t => t.TicketId);

            builder.Property(t => t.Status)
                .HasConversion(v => v.ToString(),
                    v => (TicketStatus)Enum.Parse(typeof(TicketStatus), v));
            builder.Property(t => t.Priority)
                .HasConversion(v => v.ToString(),
                    v => (TicketPriority)Enum.Parse(typeof(TicketPriority), v));
            builder.Property(t => t.DateAndTimeOfCreation);
            builder.Property(t => t.DateAndTimeOfCompletion).IsRequired(false);
            builder.Property(t => t.Title);
            builder.Property(t => t.Description);

            builder.HasMany(t => t.Comments).WithOne();

            builder.HasOne(t => t.Company)
                .WithMany(t => t.Tickets)
                .HasForeignKey(c => c.TicketId);

            builder.Property(t => t.Attachments);

            builder.HasMany(t => t.Technicians).WithMany(t => t.Tickets);

            builder.Property(t => t.TicketType)
                .HasConversion(v => v.ToString(),
                    v => (TicketType)Enum.Parse(typeof(TicketType), v));
            builder.Property(t => t.Solution);
            builder.Property(t => t.Quality);
            builder.Property(t => t.SupportNeeded);
            
            builder.HasMany(t => t.TicketChanges).WithOne();
        }
    }
}