using System;
using System.Collections.Generic;
using System.Text;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicketComment
    {
        public int TicketCommentId { get; set; }
        public ActemiumTicket Ticket { get; set; }
        public UserModel User { get; set; }
        public string UserRole { get; set; }
        public DateTime DateTimeOfComment { get; set; }
        public string CommentText { get; set; }
        
        public ActemiumTicketComment()
        {
            
        }

        
        public override string ToString()
        {
            StringBuilder ticketComment = new StringBuilder();
            ticketComment.Append(string.Format("%s: %s %s%n", UserRole, User.FirstName, User.LastName));
            ticketComment.Append(string.Format("%s: %s%n", "Date", DateTimeOfComment.ToString("dd/MM/yyy")));
            ticketComment.Append(string.Format("%s: %s%n", "Time", DateTimeOfComment.ToString("HH:mm::ss")));
            ticketComment.Append(string.Format("%s: %s%n", "Text", CommentText));
            Console.WriteLine(ticketComment.ToString());
            return ticketComment.ToString();
        }
    }
}