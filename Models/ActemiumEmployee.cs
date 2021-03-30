using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumEmployee : UserModel
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public ICollection<string> Specialties { get; set; }
        
        public ActemiumEmployee()
        {
            Specialties = new HashSet<string>();
        }
    }
}