using _2021_dotnet_e_02.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicket
    {
        public int TicketId { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime DateAndTimeOfCreation { get; set; }
        public DateTime? DateAndTimeOfCompletion { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ActemiumCompany Company { get; set; }        
        public ICollection<ActemiumTicketComment> Comments { get; set; }
        public string Attachments { get; set; }
        public ICollection<ActemiumEmployee> Technicians { get; set; }
        public TicketType TicketType { get; set; }
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
        
        public ActemiumTicket(/*TicketStatus status, TicketPriority priority,*/ string title, string description, string attachments, /*TicketType type,*/ string solution, string quality, string supportNeeded)
        {
            // Status = status;
            // Priority = priority;
            Title = title;
            Description = description;
            Attachments = attachments;
            // TicketType = type;
            Solution = solution;
            Quality = quality;
            SupportNeeded = supportNeeded;
        }

        public void EditTicket(TicketStatus status, TicketPriority priority, string title, string description, string attachments, TicketType type, string solution, string quality, string supportNeeded)
        {
            Status = status;
            Priority = priority;
            Title = title;
            Description = description;
            Attachments = attachments;
            TicketType = type;
            Solution = solution;
            Quality = quality;
            SupportNeeded = supportNeeded;
        }
        public void EditTicket(/*TicketStatus status, TicketPriority priority,*/ string title, string description, string attachments, /*TicketType type,*/ string solution, string quality, string supportNeeded)
        {
            /*Status = status;
            Priority = priority;*/
            Title = title;
            Description = description;
            Attachments = attachments;
            // TicketType = type;
            Solution = solution;
            Quality = quality;
            SupportNeeded = supportNeeded;
        }
    }
}