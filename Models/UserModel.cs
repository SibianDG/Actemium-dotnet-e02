using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<LoginAttempt> LoginAttempts { get; set; }
        public int FailedLoginAttempts { get; set; }

        public UserModel()
        {
            LoginAttempts = new List<LoginAttempt>();
        }
    }
}