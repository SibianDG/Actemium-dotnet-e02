using System;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Models
{
    public class LoginAttempt
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Username { get; set; }
        public LoginStatus LoginStatus { get; set; }
        public UserModel UserModel { get; set; }
        
        public LoginAttempt()
        {
            
        }
    }
}