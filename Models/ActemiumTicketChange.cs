using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicketChange
    {
        public int TicketChangeId { get; set; }
        //ActmiumTicket ticket
        //UserModel user
        public string userRole { get; set; }
        public DateTime DateTimeOfChange { get; set; }
        public string ChangeDescription { get; set; }
        public List<string> ChangeContent { get; set; }
        
        public ActemiumTicketChange()
        {
            
        }
    }
}