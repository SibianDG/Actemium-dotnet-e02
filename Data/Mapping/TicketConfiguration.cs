using _2021_dotnet_e_02.Models;
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

            builder.Property(t => t.Status);
            builder.Property(t => t.Priority);
            builder.Property(t => t.DateAndTimeOfCreation);
            builder.Ignore(t => t.DateAndTimeOfCompletion);
            //builder.Property(t => t.DateOfCreation);
            builder.Property(t => t.Title);
            builder.Property(t => t.Description);
            //builder.HasMany(t => t.Comments).WithOne();
            
            //builder.Ignore(t => t.Company);
            builder.HasOne(t => t.Company)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.TicketId);
            
            builder.Property(t => t.Attachments);
            builder.Ignore(t => t.Technicians);
            builder.Property(t => t.TicketType);
            builder.Property(t => t.Solution);
            builder.Property(t => t.Quality);
            builder.Property(t => t.SupportNeeded);
            builder.Ignore(t => t.TicketChanges);
        }
    }
}