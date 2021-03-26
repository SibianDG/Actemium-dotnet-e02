using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicket
    {
        public int TicketId { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DateOfCreation { get; set; }
        //Todo convert to DATe
        public string CompletionDate { get; set; }
        public DateTime DateAndTimeOfCreation { get; set; }
        public DateTime DateAndTimeOfCompletion { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //ActemiumCustomer customer
        //ActemiumCompany company
        //List<ActemiumTicketComment> comments
        public string Attachments { get; set; }
        //List<ActemiumEmployee> technicians
        public string TicketType { get; set; }
        public string Solution { get; set; }
        public string Quality { get; set; }
        public string SupportNeeded { get; set; }
        //List<ActemiumTicketChange> ticketChanges

        public ActemiumTicket()
        {
            
        }
    }
}