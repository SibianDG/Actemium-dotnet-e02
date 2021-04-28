using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class TicketChangeConfiguration : IEntityTypeConfiguration<ActemiumTicketChange>
    {
        public void Configure(EntityTypeBuilder<ActemiumTicketChange> builder)
        {
            builder.ToTable("ACTEMIUMTICKETCHANGE");

            builder.HasKey(t => t.TicketChangeId);

            builder.Property<int>("TICKET_TICKETID");
            builder.HasOne(t => t.Ticket).WithMany(t => t.TicketChanges).IsRequired().OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("TICKET_TICKETID");

            builder.Property<int>("USER_USERID");
            builder.HasOne(t => t.User).WithMany(t => t.TicketChanges).IsRequired().OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("USER_USERID");

            builder.Property(t => t.UserRole);
            builder.Property(t => t.DateTimeOfChange);
            builder.Property(t => t.ChangeDescription);

            //TODO convert string to List<String>
            // input string from db should be split after every newline char

            //builder.Property(t => t.ChangeContent).IsRequired(false)
            //        .HasConversion(
            //            v => JsonConvert.SerializeObject(v),
            //            v => JsonConvert.DeserializeObject<List<string>>(v));

            //builder.HasMany(t => t.ChangeContent).WithOne().IsRequired(false);
            builder.Ignore(t => t.ChangeContent);
        }
    }
}