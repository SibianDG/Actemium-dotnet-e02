using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicketComment
    {
        public int TicketCommentId { get; set; }
        [JsonIgnore]
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
            ticketComment.Append($"{UserRole}: {User.FirstName} {User.LastName} {Environment.NewLine}");
            ticketComment.Append($"Date: {DateTimeOfComment.ToString("dd/MM/yyyy")} {Environment.NewLine}");
            ticketComment.Append($"Date: {DateTimeOfComment.ToString("HH:mm:ss")} {Environment.NewLine}");
            ticketComment.Append($"Text: {CommentText} {Environment.NewLine}");
            return ticketComment.ToString();
        }
    }
}