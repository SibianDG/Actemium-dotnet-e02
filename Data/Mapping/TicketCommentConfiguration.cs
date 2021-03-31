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
            
            builder.HasOne(t => t.Ticket).WithMany(t => t.Comments).IsRequired().OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(t=> t.TicketCommentId);
            builder.HasOne(t => t.User).WithMany(t => t.Comments);
            builder.Property(t => t.UserRole);
            builder.Property(t => t.DateTimeOfComment);
            builder.Property(t => t.CommentText);
        }
    }
}