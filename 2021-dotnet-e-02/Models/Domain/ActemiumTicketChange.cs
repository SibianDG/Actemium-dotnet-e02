using System;
using System.Collections.Generic;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Models.ViewModels.TicketViewModel;

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
        //public ICollection<string> ChangeContent { get; set; }
        public ICollection<ActemiumTicketChangeContent> ChangeContents { get; set; }
        
        public ActemiumTicketChange()
        {
            ChangeContents = new List<ActemiumTicketChangeContent>();
            //ChangeContent = new List<string>();
        }
        public ActemiumTicketChange(ActemiumTicket ticket, UserModel user, EditViewModel ticketCopy, bool isSupportManager, bool modifying)
        
        {
            ChangeContents = new List<ActemiumTicketChangeContent>();
            string type = ticket.Status.Equals(TicketStatus.CANCELLED) || ticket.Status.Equals(TicketStatus.COMPLETED)
                ? "completed"
                : "outstanding";
            string what = modifying ? "Modified" : "Deleted";
            ChangeDescription = $"{what} {type} ticket with ID: {ticket.TicketId}";
            DateTimeOfChange = DateTime.Now;
            User = user;
            UserRole = isSupportManager ? "Support Manager" : "Customer";

            if (ticketCopy != null)
            {
                if (ticket.Title != ticketCopy.Title)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Title changed from \"{ticketCopy.Title}\" to \"{ticket.Title}\"."));
                if (ticket.Attachments != ticketCopy.Attachments)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Attachments changed from \"{ticketCopy.Attachments}\" to \"{ticket.Attachments}\"."));
                if (ticket.DateAndTimeOfCreation != ticketCopy.DateAndTimeOfCreation)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket DateAndTimeOfCreation changed from \"{ticketCopy.DateAndTimeOfCreation}\" to \"{ticket.DateAndTimeOfCreation}\"."));
                if (ticket.DateAndTimeOfCompletion != ticketCopy.DateAndTimeOfCompletion)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket DateAndTimeOfCompletion changed from \"{ticketCopy.DateAndTimeOfCompletion}\" to \"{ticket.DateAndTimeOfCompletion}\"."));
                if (ticket.Description != ticketCopy.Description)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Description changed from \"{ticketCopy.Description}\" to \"{ticket.Description}\"."));
                if (ticket.Priority != ticketCopy.Priority)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Priority changed from \"{ticketCopy.Priority}\" to \"{ticket.Priority}\"."));
                if (ticket.Quality != ticketCopy.Quality)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Quality changed from \"{ticketCopy.Quality}\" to \"{ticket.Quality}\"."));
                if (ticket.Solution != ticketCopy.Solution)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Solution changed from \"{ticketCopy.Solution}\" to \"{ticket.Solution}\"."));
                if (ticket.Status != ticketCopy.Status)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Status changed from \"{ticketCopy.Status}\" to \"{ticket.Status}\"."));
                if (ticket.SupportNeeded != ticketCopy.SupportNeeded)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket SupportNeeded changed from \"{ticketCopy.SupportNeeded}\" to \"{ticket.SupportNeeded}\"."));
                if (ticket.TicketType != ticketCopy.TicketType)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket TicketType changed from \"{ticketCopy.TicketType}\" to \"{ticket.TicketType}\"."));
            }
            else
            {
                ChangeContents.Add(new ActemiumTicketChangeContent(this,
                    $"Ticket TicketStatus changed from \"{ticket.Status}\" to \"{TicketStatus.CANCELLED}\"."));
            }
        }
        
    }
}