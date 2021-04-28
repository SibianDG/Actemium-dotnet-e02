using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class TicketChangeContentConfiguration : IEntityTypeConfiguration<ActemiumTicketChangeContent>
    {
        public void Configure(EntityTypeBuilder<ActemiumTicketChangeContent> builder)
        {
            builder.ToTable("ACTEMIUMTICKETCHANGECONTENT");

            builder.HasKey(c => c.TicketChangeContentId);

            builder.Property<int>("TICKETCHANGE_TICKETCHANGEID");
            builder.HasOne(t => t.TicketChange).WithMany(t => t.ChangeContents).IsRequired(false).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("TICKETCHANGE_TICKETCHANGEID");

            builder.Property(c => c.ChangeContent);
        }
    }
}