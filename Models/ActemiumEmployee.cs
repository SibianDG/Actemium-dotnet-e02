using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumEmployee
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        //Set<TicketType> specialties
        
        public ActemiumEmployee()
        {
            
        }
    }
}