using _2021_dotnet_e_02.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicket
    {
        public int TicketId { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DateAndTimeOfCreation { get; set; }
        public DateTime DateAndTimeOfCompletion { get; set; }
        //public DateTime DateOfCreation { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ActemiumCompany Company { get; set; }        
        public ICollection<ActemiumTicketComment> Comments { get; set; }
        public string Attachments { get; set; }
        public ICollection<ActemiumEmployee> Technicians { get; set; }
        public string TicketType { get; set; }
        public string Solution { get; set; }
        public string Quality { get; set; }
        public string SupportNeeded { get; set; }
        public ICollection<ActemiumTicketChange> TicketChanges { get; set; }

        public ActemiumTicket()
        {
            Comments = new List<ActemiumTicketComment>();
            Technicians = new List<ActemiumEmployee>();
            TicketChanges = new List<ActemiumTicketChange>();
        }
    }
}