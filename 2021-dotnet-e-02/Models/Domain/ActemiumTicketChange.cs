using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicketChange
    {
        public int TicketChangeId { get; set; }
        public ActemiumTicket Ticket { get; set; }
        public UserModel User { get; set; }
        public string UserRole { get; set; }
        public DateTime DateTimeOfChange { get; set; }
        public string ChangeDescription { get; set; }
        //TODO convert string to List<String>
        // input string from db should be split after every newline char
        //public string ChangeContent { get; set; }
        public ICollection<string> ChangeContent { get; set; }
        //public ICollection<ActemiumTicketChangeContent> ChangeContent { get; set; }

        public ActemiumTicketChange()
        {
            //ChangeContent = new List<ActemiumTicketChangeContent>();
            ChangeContent = new List<string>();
        }
    }
}