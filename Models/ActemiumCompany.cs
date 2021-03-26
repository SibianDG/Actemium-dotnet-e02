using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumCompany
    {
        
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; } 
        
        //List<ActemiumCustomer> ContactPersons
        //List<ActemiumTikcet> ActemiumTickets
        
        public ActemiumCompany()
        {
            
        }
    }
}