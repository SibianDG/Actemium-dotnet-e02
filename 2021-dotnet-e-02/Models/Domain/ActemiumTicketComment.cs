using System;
using System.Collections.Generic;

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
    }
}