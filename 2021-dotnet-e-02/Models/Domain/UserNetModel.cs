using _2021_dotnet_e_02.Models.Enums;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace _2021_dotnet_e_02.Models
{
    public class UserNetModel : IdentityUser
    {
        public int UserId { get; set; }
        //public UserModel UserModel { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<LoginAttempt> LoginAttempts { get; set; }
        public int FailedLoginAttempts { get; set; }
        [JsonIgnore]
        public ICollection<ActemiumTicketComment> Comments { get; set; }
        public ICollection<ActemiumTicketChange> TicketChanges { get; set; }

        public ICollection<ActemiumTicketActemiumUser> TicketTechnicians { get; set; }

        public UserNetModel()
        {
            LoginAttempts = new List<LoginAttempt>();
            Comments = new HashSet<ActemiumTicketComment>();
            TicketChanges = new HashSet<ActemiumTicketChange>();
            TicketTechnicians = new List<ActemiumTicketActemiumUser>();
        }
    }
}