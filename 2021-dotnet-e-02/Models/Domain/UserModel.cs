using _2021_dotnet_e_02.Models.Enums;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _2021_dotnet_e_02.Models
{
    public class UserModel //: ApplicationUser
    {
        public int UserId { get; internal set; }
        public string UserName { get; set; }
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

        public UserModel()
        {
            LoginAttempts = new List<LoginAttempt>();
            Comments = new HashSet<ActemiumTicketComment>();
            TicketChanges = new HashSet<ActemiumTicketChange>();
            TicketTechnicians = new List<ActemiumTicketActemiumUser>();
        }

        public UserModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
            FirstName = "Pol";
            LastName = "Thijs";
            Status = UserStatus.ACTIVE;
            RegistrationDate = DateTime.Now;
            LoginAttempts = new List<LoginAttempt>();
            FailedLoginAttempts = 0;
            Comments = new HashSet<ActemiumTicketComment>();
            TicketChanges = new HashSet<ActemiumTicketChange>();
            TicketTechnicians = new List<ActemiumTicketActemiumUser>();
        }

        public UserModel(string userName, string password, string firstName, string lastName, UserStatus status, DateTime registrationDate, ICollection<LoginAttempt> loginAttempts, int failedLoginAttempts)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Status = status;
            RegistrationDate = registrationDate;
            LoginAttempts = loginAttempts;
            FailedLoginAttempts = failedLoginAttempts;
        }
    }
}