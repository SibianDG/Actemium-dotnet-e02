using _2021_dotnet_e_02.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;


namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicket
    {
        public int TicketId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketStatus Status { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketPriority Priority { get; set; }
        [Display(Name = "Date and time of creation")]
        public DateTime DateAndTimeOfCreation { get; set; }
        [Display(Name = "Date and time of completion")]
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
        public ICollection<ActemiumTicketActemiumUser> TicketTechnicians { get; set; }
        [Required(ErrorMessage = "Cannot add an empty comment")]
        [Display(Name = "Add new comment")]
        public string NewComment { get; set; }

        public ActemiumTicket()
        {
            Comments = new List<ActemiumTicketComment>();
            Technicians = new List<ActemiumEmployee>();
            TicketChanges = new List<ActemiumTicketChange>();
            TicketTechnicians = new List<ActemiumTicketActemiumUser>();
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
            DateAndTimeOfCreation = DateTime.Now;
            //Solution = solution;
            //Quality = quality;
            //SupportNeeded = supportNeeded;

            // We need to initialize the lists here because we instantly go to FullDetails after creation
            Comments = new List<ActemiumTicketComment>();
            Technicians = new List<ActemiumEmployee>();
            TicketChanges = new List<ActemiumTicketChange>();
            TicketTechnicians = new List<ActemiumTicketActemiumUser>();
        }

        public void EditTicket(TicketStatus status, TicketPriority priority, string title, string description, string attachments, TicketType type)
        {
            // Status cannot be edited by the customer
            // Status can be edited by the support manager
            Status = status;
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
        public void EditTicketCompleted(TicketStatus status, TicketPriority priority, string title, string description, string attachments, TicketType type, string solution, string quality, string supportNeeded)
        {
            // Status cannot be edited by the customer
            // Status can be edited by the support manager
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

        public void AddNewComment(ActemiumTicket ticket, UserModel user, string userRole, string newCommentText)
        {
            ActemiumTicketComment newComment = new ActemiumTicketComment(ticket, user, userRole, newCommentText);
            Console.WriteLine(newComment.CommentText);
            Comments.Add(newComment);
        }
    }
}