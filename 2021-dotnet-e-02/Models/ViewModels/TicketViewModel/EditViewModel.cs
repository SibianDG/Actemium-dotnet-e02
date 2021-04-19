using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Models.ViewModels.TicketViewModel
{
    public class EditViewModel
    {
        //public int TicketId { get; set; }
        
         [Required(ErrorMessage = "Ticket status is required")]
         [Display(Name = "Ticket status")]
         [EnumDataType(typeof(TicketStatus))]
         public TicketStatus Status { get; set; }
        
         [Required(ErrorMessage = "Ticket priority is required")]
         [Display(Name = "Ticket priority")]
         [EnumDataType(typeof(TicketPriority))]
         public TicketPriority Priority { get; set; }
        public DateTime DateAndTimeOfCreation { get; set; }
        public DateTime? DateAndTimeOfCompletion { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Description")]
        [StringLength(255, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 10)]
        public string Description { get; set; }
        //public ActemiumCompany Company { get; set; }        
        //public ICollection<ActemiumTicketComment> Comments { get; set; }
        
        [Display(Name = "Attachments")]
        [StringLength(255, ErrorMessage = "The {0} can't be longer than {0} characters.")]
        public string Attachments { get; set; }
        //public ICollection<ActemiumEmployee> Technicians { get; set; }
        
        [Required(ErrorMessage = "Ticket type is required")]
        [Display(Name = "Ticket type")]
        [EnumDataType(typeof(TicketType))]
        public TicketType TicketType { get; set; }
        
        [Display(Name = "Solution")]
        [StringLength(255, ErrorMessage = "The {0} can't be longer than {0} characters.")]
        public string Solution { get; set; }
        
        [Display(Name = "Quality")]
        [StringLength(255, ErrorMessage = "The {0} can't be longer than {0} characters.")]
        public string Quality { get; set; }
        
        [Display(Name = "SupportNeeded")]
        [StringLength(255, ErrorMessage = "The {0} can't be longer than {0} characters.")]
        public string SupportNeeded { get; set; }
        //public ICollection<ActemiumTicketChange> TicketChanges { get; set; }

        public EditViewModel()
        {
        }

        public EditViewModel(ActemiumTicket ticket)
        {
            //TicketId = ticket.TicketId;
            Status = ticket.Status;
            Priority = ticket.Priority;
            Title = ticket.Title;
            Attachments = ticket.Attachments;
            Description = ticket.Description;
            TicketType = ticket.TicketType;
            Solution = ticket.Solution;
            Quality = ticket.Quality;
            SupportNeeded = ticket.SupportNeeded;

        }
    }
}