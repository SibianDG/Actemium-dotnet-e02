using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicket
    {
        public int TicketId { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        // Has been removed in java and thus removed in db
        // because it is duplicate data
        // public DateTime DateOfCreation { get; set; }
        // Todo convert to DATe => NO
        // completionDate was only a stringproperty in java
        // it is not stored in the db because that would be
        // duplicate data since we already store 
        // DateAndTimeOfCompletion
        // public string CompletionDate { get; set; }
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