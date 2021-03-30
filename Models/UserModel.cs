using System;
using System.Collections.Generic;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace _2021_dotnet_e_02.Models
{
    public class UserModel //: IdentityUser
    {
        public int UserId { get; set; }
        /*override*/ public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<LoginAttempt> LoginAttempts { get; set; }
        public int FailedLoginAttempts { get; set; }

        public UserModel()
        {
            LoginAttempts = new List<LoginAttempt>();
        }
    }
}