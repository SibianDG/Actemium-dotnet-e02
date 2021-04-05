using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Models.ViewModels.TicketViewModel
{
    public class EditViewModel
    {
        //public int TicketId { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        //public DateTime DateAndTimeOfCreation { get; set; }
        //public DateTime? DateAndTimeOfCompletion { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public ActemiumCompany Company { get; set; }        
        //public ICollection<ActemiumTicketComment> Comments { get; set; }
        public string Attachments { get; set; }
        //public ICollection<ActemiumEmployee> Technicians { get; set; }
        public TicketType TicketType { get; set; }
        public string Solution { get; set; }
        public string Quality { get; set; }
        public string SupportNeeded { get; set; }
        //public ICollection<ActemiumTicketChange> TicketChanges { get; set; }

        public EditViewModel(ActemiumTicket ticket)
        {
            //TicketId = ticket.TicketId;
            Status = ticket.Status;
            Priority = ticket.Priority;
            Title = ticket.Title;
            Attachments = ticket.Attachments;
            TicketType = ticket.TicketType;
            Solution = ticket.Solution;
            Quality = ticket.Quality;
            SupportNeeded = ticket.SupportNeeded;

        }
    }
}