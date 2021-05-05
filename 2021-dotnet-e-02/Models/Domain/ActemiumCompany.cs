using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        public ICollection<ActemiumCustomer> ContactPersons { get; set; }
        [JsonIgnore]
        public ICollection<ActemiumTicket> Tickets { get; set; }
        [JsonIgnore]
        public ICollection<ActemiumContract> Contracts { get; set; }

        public ActemiumCompany()
        {
            Tickets = new List<ActemiumTicket>();
            ContactPersons = new List<ActemiumCustomer>();
            Contracts = new List<ActemiumContract>();
        }

        public void addActemiumTicket(ActemiumTicket actemiumTicket) => Tickets.Add(actemiumTicket);

    }
}