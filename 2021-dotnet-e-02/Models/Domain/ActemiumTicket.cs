using _2021_dotnet_e_02.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Converters;

// Make SportsStore.tests a friendly assembly so it can access the internal properties of this class
//[assembly: InternalsVisibleTo("2021-dotnet-e-02.Tests")]

namespace _2021_dotnet_e_02.Models
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class ActemiumTicket
    {
        //[JsonProperty]
        public int TicketId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketStatus Status { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketPriority Priority { get; set; }
        public DateTime DateAndTimeOfCreation { get; set; }
        public DateTime? DateAndTimeOfCompletion { get; set; }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title must have a value and can't be null", nameof(Title));
                _title = value;
            } 
        }
        //public string Title { get; set; }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Description must have a value and can't be null", nameof(Description));
                _description = value;
            }
        }
        //public string Description { get; set; }
        public ActemiumCompany Company { get; set; }
        public ICollection<ActemiumTicketComment> Comments { get; set; }
        public string Attachments { get; set; }
        public ICollection<ActemiumEmployee> Technicians { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketType TicketType { get; set; }
        public string Solution { get; set; }
        public string Quality { get; set; }
        public string SupportNeeded { get; set; }
        public ICollection<ActemiumTicketChange> TicketChanges { get; set; }

        //[JsonConstructor]
        //private ActemiumTicket(int ticketId)
        //{
        //    TicketId = ticketId;
        //}

        public ActemiumTicket()
        {
            Comments = new List<ActemiumTicketComment>();
            Technicians = new List<ActemiumEmployee>();
            TicketChanges = new List<ActemiumTicketChange>();
        }

        public ActemiumTicket(TicketStatus status, TicketPriority priority, string title, ActemiumCompany company, string description, string attachments, TicketType type/*, string solution, string quality, string supportNeeded*/)
        {
            Status = status;
            Priority = priority;
            Title = title;
            Company = company;
            Description = description;
            Attachments = attachments;
            TicketType = type;
            //Solution = solution;
            //Quality = quality;
            //SupportNeeded = supportNeeded;
        }

        public void EditTicket(TicketPriority priority, string title, string description, string attachments, TicketType type)
        {
            // Status cannot be edited by the customer
            //Status = status;
            Priority = priority;
            Title = title;
            Description = description;
            Attachments = attachments;
            TicketType = type;
            // The attributes below can only be editted when the ticket has been completed
            //Solution = solution;
            //Quality = quality;
            //SupportNeeded = supportNeeded;
        }

        // method not used yet, but don't remove it because we will need it
        public void EditTicketCompleted(TicketPriority priority, string title, string description, string attachments, TicketType type, string solution, string quality, string supportNeeded)
        {
            // Status cannot be edited by the customer
            //Status = status;
            Priority = priority;
            Title = title;
            Description = description;
            Attachments = attachments;
            TicketType = type;
            Solution = solution;
            Quality = quality;
            SupportNeeded = supportNeeded;
        }
        // public void EditTicket(/*TicketStatus status, TicketPriority priority,*/ string title, string description, string attachments, /*TicketType type,*/ string solution, string quality, string supportNeeded)
        // {
        //     /*Status = status;
        //     Priority = priority;*/
        //     Title = title;
        //     Description = description;
        //     Attachments = attachments;
        //     // TicketType = type;
        //     Solution = solution;
        //     Quality = quality;
        //     SupportNeeded = supportNeeded;
        // }
    }
}