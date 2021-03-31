using System;
using System.Collections.Generic;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumEmployee : UserModel
    {
        public string Address { get; set; }
        /*override*/ public string PhoneNumber { get; set; }
        /*override*/ public string Email { get; set; }
        public EmployeeRole Role { get; set; }
        public ICollection<string> Specialties { get; set; }
        
        public ActemiumEmployee()
        {
            Specialties = new HashSet<string>();
        }
    }
}