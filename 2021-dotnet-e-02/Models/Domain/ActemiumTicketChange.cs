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
        public ICollection<ActemiumTicketChangeContent> ChangeContents { get; set; }
        
        public ActemiumTicketChange()
        {
            ChangeContents = new List<ActemiumTicketChangeContent>();
        }

        // Constructor for change when ticket is modified
        public ActemiumTicketChange(ActemiumTicket ticketOld, UserModel user, EditViewModel ticketNew, bool isSupportManager, bool modifying)        
        {
            ChangeContents = new List<ActemiumTicketChangeContent>();
            string type = ticketOld.Status.Equals(TicketStatus.CANCELLED) || ticketOld.Status.Equals(TicketStatus.COMPLETED)
                ? "completed"
                : "outstanding";
            string what = modifying ? "Modified" : "Deleted";
            ChangeDescription = $"{what} {type} ticket with ID: {ticketOld.TicketId}";
            DateTimeOfChange = DateTime.Now;
            User = user;
            UserRole = isSupportManager ? "Support Manager" : "Customer";

            if (ticketNew != null)
            {
                if (ticketNew.Title != ticketOld.Title)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Title changed from \"{ticketOld.Title}\" to \"{ticketNew.Title}\"."));
                if (ticketNew.Attachments != ticketOld.Attachments)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Attachments changed from \"{ticketOld.Attachments}\" to \"{ticketNew.Attachments}\"."));
                if (ticketNew.Description != ticketOld.Description)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Description changed from \"{ticketOld.Description}\" to \"{ticketNew.Description}\"."));
                if (ticketNew.Priority != ticketOld.Priority)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Priority changed from \"{ticketOld.Priority}\" to \"{ticketNew.Priority}\"."));
                if (ticketNew.Status != ticketOld.Status)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Status changed from \"{ticketOld.Status}\" to \"{ticketNew.Status}\"."));
                if (ticketNew.TicketType != ticketOld.TicketType)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"TicketType changed from \"{ticketOld.TicketType}\" to \"{ticketNew.TicketType}\"."));
                if (ticketOld.Quality != null && ticketNew.Quality != null) 
                    if (!ticketNew.Quality.Trim().Equals(ticketOld.Quality.Trim()))
                        ChangeContents.Add(new ActemiumTicketChangeContent(this,
                            $"Quality changed from \"{ticketOld.Quality}\" to \"{ticketNew.Quality}\"."));
                if (ticketOld.Solution != null && ticketNew.Solution != null)
                    if (!ticketNew.Solution.Trim().Equals(ticketOld.Solution.Trim()))
                        ChangeContents.Add(new ActemiumTicketChangeContent(this,
                            $"Solution changed from \"{ticketOld.Solution}\" to \"{ticketNew.Solution}\"."));
                if (ticketOld.SupportNeeded != null && ticketNew.SupportNeeded != null)
                    if (!ticketNew.SupportNeeded.Trim().Equals(ticketOld.SupportNeeded.Trim()))
                        ChangeContents.Add(new ActemiumTicketChangeContent(this,
                            $"SupportNeeded changed from \"{ticketOld.SupportNeeded}\" to \"{ticketNew.SupportNeeded}\"."));
            }
            else
            {
                ChangeContents.Add(new ActemiumTicketChangeContent(this,
                    $"TicketStatus changed from \"{ticketOld.Status}\" to \"{TicketStatus.CANCELLED}\"."));
            }
        }

        // Constructor for change when ticket is created
        public ActemiumTicketChange(ActemiumTicket ticket, UserModel user, bool isSupportManager)
        {
            ChangeContents = new List<ActemiumTicketChangeContent>();
            // we need to add the ticket to the db before we can know its id
            // i did a workaround for this in java, but it's a lot of work for a small detail
            //ChangeDescription = $"Created the ticket with ID: {ticket.TicketId}"; 
            ChangeDescription = $"Created the ticket";
            DateTimeOfChange = DateTime.Now;
            User = user;
            UserRole = isSupportManager ? "Support Manager" : "Customer";

            ChangeContents.Add(new ActemiumTicketChangeContent(this,
                $"Title was set to \"{ticket.Title}\"."));

            ChangeContents.Add(new ActemiumTicketChangeContent(this,
                $"Priority was set to \"{ticket.Priority}\"."));

            ChangeContents.Add(new ActemiumTicketChangeContent(this,
                $"Status was set to \"{ticket.Status}\"."));

            ChangeContents.Add(new ActemiumTicketChangeContent(this,
                $"TicketType was set to \"{ticket.TicketType}\"."));

            ChangeContents.Add(new ActemiumTicketChangeContent(this,
                $"Description was set to \"{ticket.Description}\"."));

            ChangeContents.Add(new ActemiumTicketChangeContent(this,
                $"Attachments were added: \"{ticket.Attachments}\"."));
        }

    }
}