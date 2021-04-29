using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class TicketCommentConfiguration : IEntityTypeConfiguration<ActemiumTicketComment>
    {
        public void Configure(EntityTypeBuilder<ActemiumTicketComment> builder)
        {
            builder.ToTable("ACTEMIUMTICKETCOMMENT");

            builder.HasKey(t => t.TicketCommentId);

            builder.Property<int>("TICKET_TICKETID");
            builder.HasOne(t => t.Ticket).WithMany(t => t.Comments).IsRequired().OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("TICKET_TICKETID");
            builder.Property<int>("USER_USERID");
            builder.HasOne(t => t.User).WithMany(t => t.Comments).HasForeignKey("USER_USERID");
            //builder.Property(t => t.User).HasColumnName("USER_USERID");
            builder.Property(t => t.UserRole);
            builder.Property(t => t.DateTimeOfComment);
            builder.Property(t => t.CommentText);
        }
    }
}