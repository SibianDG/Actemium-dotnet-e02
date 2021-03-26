using System;

namespace _2021_dotnet_e_02.Models
{
    public class LoginAttempt
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Username { get; set; }
        private string LoginStatus { get; set; }
        //UserModel userModel
        
        public LoginAttempt()
        {
            
        }
    }
}