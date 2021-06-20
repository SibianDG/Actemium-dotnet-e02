using _2021_dotnet_e_02.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using _2021_dotnet_e_02.Models.ViewModels.TicketViewModel;


namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicket
    {
        public int TicketId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketStatus Status { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketPriority Priority { get; set; }
        [Display(Name = "Time of creation")]
        public DateTime DateAndTimeOfCreation { get; set; }
        [Display(Name = "Time of completion")]
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
        //[JsonIgnore]
        public ActemiumCompany Company { get; set; }
        [JsonIgnore]
        public ICollection<ActemiumTicketComment> Comments { get; set; }
        public string Attachments { get; set; }
        public ICollection<ActemiumEmployee> Technicians { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TicketType TicketType { get; set; }
        public string Solution { get; set; }
        public string Quality { get; set; }
        public string SupportNeeded { get; set; }
        [JsonIgnore]
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
            Solution = "";
            Quality = "";
            SupportNeeded = "";

            // We need to initialize the lists here because we instantly go to FullDetails after creation
            Comments = new List<ActemiumTicketComment>();
            Technicians = new List<ActemiumEmployee>();
            TicketChanges = new List<ActemiumTicketChange>();
            TicketTechnicians = new List<ActemiumTicketActemiumUser>();
        }

        public void EditTicket(TicketStatus status, TicketPriority priority, string title, string description, string attachments, TicketType type, DateTime? dateAndTimeOfCompletion)
        {
            // Status cannot be edited by the customer
            // Status can be edited by the support manager
            Status = status;
            Priority = priority;
            Title = title;
            Description = description;
            Attachments = attachments;
            TicketType = type;
            DateAndTimeOfCompletion = dateAndTimeOfCompletion;
            // The attributes below can only be editted when the ticket has been completed
            //Solution = solution;
            //Quality = quality;
            //SupportNeeded = supportNeeded;
        }

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

        public void EditTicketCompletedAsCustomer(TicketStatus status, TicketPriority priority, string title, string description, string attachments, TicketType type, string quality)
        {
            // Status cannot be edited by the customer
            // Status can be edited by the support manager
            Status = status;
            Priority = priority;
            Title = title;
            Description = description;
            Attachments = attachments;
            TicketType = type;
            Quality = quality;
        }

        public void AddNewComment(ActemiumTicket ticket, UserModel user, string userRole, string newCommentText)
        {
            ActemiumTicketComment newComment = new ActemiumTicketComment(ticket, user, userRole, newCommentText);
            Console.WriteLine(newComment.CommentText);
            Comments.Add(newComment);
        }

        public static bool EqualsTicket(EditViewModel newTicket, ActemiumTicket oldTicket)
        {
            if ((newTicket == null || oldTicket == null))
            {
                return false;
            }

            return (
                oldTicket.TicketId != newTicket.TicketId ||
                oldTicket.Status != newTicket.Status ||
                oldTicket.Priority != newTicket.Priority ||
                oldTicket.DateAndTimeOfCreation != newTicket.DateAndTimeOfCreation ||
                oldTicket.DateAndTimeOfCompletion != newTicket.DateAndTimeOfCompletion ||
                oldTicket.Title != newTicket.Title ||
                oldTicket.Description != newTicket.Description ||
                oldTicket.Attachments != newTicket.Attachments ||
                oldTicket.TicketType != newTicket.TicketType ||
                oldTicket.Solution != newTicket.Solution ||
                oldTicket.Quality != newTicket.Quality ||
                oldTicket.SupportNeeded != newTicket.SupportNeeded //||
                //oldTicket.Technicians.Count != newTicket.
            );
        }
        
        public static ActemiumTicket Clone(ActemiumTicket source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            ActemiumTicket t = JsonConvert.DeserializeObject<ActemiumTicket>(serialized);
            Console.WriteLine("Title after clone: "+t.Title);
            return t;
        }
    }
}