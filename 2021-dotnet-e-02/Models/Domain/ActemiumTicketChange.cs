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
                        $"Ticket Title changed from \"{ticketOld.Title}\" to \"{ticketNew.Title}\"."));
                if (ticketNew.Attachments != ticketOld.Attachments)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Attachments changed from \"{ticketOld.Attachments}\" to \"{ticketNew.Attachments}\"."));
                //if (ticket.DateAndTimeOfCreation != ticketOld.DateAndTimeOfCreation)
                //    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                //        $"Ticket DateAndTimeOfCreation changed from \"{ticketOld.DateAndTimeOfCreation}\" to \"{ticket.DateAndTimeOfCreation}\"."));
                //if (ticket.DateAndTimeOfCompletion != ticketOld.DateAndTimeOfCompletion)
                //    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                //        $"Ticket DateAndTimeOfCompletion changed from \"{ticketOld.DateAndTimeOfCompletion}\" to \"{ticket.DateAndTimeOfCompletion}\"."));
                if (ticketNew.Description != ticketOld.Description)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Description changed from \"{ticketOld.Description}\" to \"{ticketNew.Description}\"."));
                if (ticketNew.Priority != ticketOld.Priority)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Priority changed from \"{ticketOld.Priority}\" to \"{ticketNew.Priority}\"."));
                if (ticketNew.Status != ticketOld.Status)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket Status changed from \"{ticketOld.Status}\" to \"{ticketNew.Status}\"."));
                if (ticketNew.TicketType != ticketOld.TicketType)
                    ChangeContents.Add(new ActemiumTicketChangeContent(this,
                        $"Ticket TicketType changed from \"{ticketOld.TicketType}\" to \"{ticketNew.TicketType}\"."));
                if (ticketOld.Quality != null && ticketNew.Quality != null) 
                    if (!ticketNew.Quality.Trim().Equals(ticketOld.Quality.Trim()))
                        ChangeContents.Add(new ActemiumTicketChangeContent(this,
                            $"Ticket Quality changed from \"{ticketOld.Quality}\" to \"{ticketNew.Quality}\"."));
                if (ticketOld.Solution != null && ticketNew.Solution != null)
                    if (!ticketNew.Solution.Trim().Equals(ticketOld.Solution.Trim()))
                        ChangeContents.Add(new ActemiumTicketChangeContent(this,
                            $"Ticket Solution changed from \"{ticketOld.Solution}\" to \"{ticketNew.Solution}\"."));
                if (ticketOld.SupportNeeded != null && ticketNew.SupportNeeded != null)
                    if (!ticketNew.SupportNeeded.Trim().Equals(ticketOld.SupportNeeded.Trim()))
                        ChangeContents.Add(new ActemiumTicketChangeContent(this,
                            $"Ticket SupportNeeded changed from \"{ticketOld.SupportNeeded}\" to \"{ticketNew.SupportNeeded}\"."));
            }
            else
            {
                ChangeContents.Add(new ActemiumTicketChangeContent(this,
                    $"Ticket TicketStatus changed from \"{ticketOld.Status}\" to \"{TicketStatus.CANCELLED}\"."));
            }
        }
        
    }
}