using System;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class TicketUserConfiguration : IEntityTypeConfiguration<ActemiumTicketActemiumUser>
    {
        public void Configure(EntityTypeBuilder<ActemiumTicketActemiumUser> builder)
        {
            builder.ToTable("ActemiumTicket_ActemiumEmployee");

            //builder.Property<int>("ActemiumTicket_TICKETID");
            //builder.Property<int>("technicians_USERID");

            //builder
            //.HasKey(new String[] { "ActemiumTicket_TICKETID", "technicians_USERID" });

            //builder.Property<int>("ActemiumTicket_TICKETID");
            //builder
            //    .HasOne(tu => tu.Ticket)
            //    .WithMany(t => t.TicketTechnicians)
            //    .HasForeignKey("ActemiumTicket_TICKETID");

            //builder.Property<int>("technicians_USERID");
            //builder
            //    .HasOne(tu => tu.Technician)
            //    .WithMany(u => u.TicketTechnicians)
            //    .HasForeignKey("technicians_USERID");

            builder
            .HasKey(t => new { t.TicketId, t.UserId });

            builder
                .HasOne(tu => tu.Ticket)
                .WithMany(t => t.TicketTechnicians)
                .HasForeignKey(tu => tu.TicketId);

            builder
                .HasOne(tu => tu.Technician)
                .WithMany(u => u.TicketTechnicians)
                .HasForeignKey(tu => tu.UserId);
        }
        
    }
}
