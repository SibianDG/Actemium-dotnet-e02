using System;
using System.Collections;
using System.Collections.Generic;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Tests.Data
{
    public class DummyApplicationDbContext
    {
        public IEnumerable<ActemiumTicket> Tickets { get; }
        public IEnumerable<ActemiumTicket> OpenTickets { get; }
        public IEnumerable<ActemiumTicket> ResolvedTickets { get; }

        public IEnumerable<ActemiumContract> Contracts { get; }

        public ActemiumTicket Ticket1 { get; } 
        public ActemiumTicket Ticket2 { get; }
        public ActemiumTicket Ticket3 { get; }
        public ActemiumTicket Ticket4 { get; }
        public ActemiumTicket Ticket5 { get; }

        public ActemiumContract Contract1 { get; }
        public ActemiumContract Contract2 { get; }
        public ActemiumContract Contract3 { get; }
        public ActemiumContract Contract4 { get; }

        public ActemiumContractType ContractType1 { get; }
        public ActemiumContractType ContractType2 { get; }
        public IEnumerable<ActemiumContractType> ContractTypes { get; }

        public ActemiumCompany Google { get; }
        public ActemiumCompany Amazon { get; }
        
        public ActemiumEmployee Admin { get; }
        public ActemiumEmployee Tech { get; }
        public ActemiumEmployee Supp { get; }
        
        public ActemiumCustomer Cust1 { get; }
        public ActemiumCustomer Cust2 { get; }

        public DummyApplicationDbContext()
        {
            int ticketId = 1;
            int companyId = 1;

            Google = new ActemiumCompany()
            {
                Name = "Google",
                CompanyId = companyId++
            };
            
            Amazon = new ActemiumCompany()
            {
                Name = "Amazon",
                CompanyId = companyId
            };
            
            //TicketStatus status, TicketPriority priority, string title, string description, string attachments, TicketType type, string solution, string quality, string supportNeeded
            Ticket1 = new ActemiumTicket(TicketStatus.CREATED, TicketPriority.P1, "Title", Google, "Description", "Attachments", TicketType.HARDWARE) {TicketId = ticketId++};
            Ticket2 = new ActemiumTicket(TicketStatus.IN_PROGRESS, TicketPriority.P2, "Title2", Google, "Description2", "Attachments2", TicketType.NETWORK){TicketId = ticketId++};
            Ticket3 = new ActemiumTicket(TicketStatus.IN_DEVELOPMENT, TicketPriority.P3, "Title3", Google, "Description3", "Attachments3", TicketType.DATABASE){TicketId = ticketId++};
            Ticket4 = new ActemiumTicket(TicketStatus.CANCELLED, TicketPriority.P1, "Title4", Amazon, "Description4", "Attachments4", TicketType.SOFTWARE){TicketId = ticketId++};
            Ticket5 = new ActemiumTicket(TicketStatus.COMPLETED, TicketPriority.P2, "Title5", Amazon, "Description5", "Attachments5", TicketType.INFRASTRUCTURE){TicketId = ticketId};

            Tickets = new[] {Ticket1, Ticket2, Ticket3, Ticket4, Ticket5};
            OpenTickets = new[] { Ticket1, Ticket2, Ticket3 };
            ResolvedTickets = new[] {Ticket5 };

            ContractType1 = new ActemiumContractType() { ContractTypeId = 1, HasApplication = true, HasEmail = true, HasPhone = false, MaxHandlingTime = 3, MinThroughputTime = 2, Name = "TestContractType1", Price = 25.00, Status = ContractTypeStatus.ACTIVE };
            ContractType2 = new ActemiumContractType() { ContractTypeId = 2, HasApplication = true, HasEmail = false, HasPhone = false, MaxHandlingTime = 4, MinThroughputTime = 1, Name = "TestContractType2", Price = 15.50, Status = ContractTypeStatus.ACTIVE };
            Contract1 = new ActemiumContract() { ContractId = 1, Company = Google, ContractType = ContractType1, EndDate = new DateTime().AddYears(1), StartDate = new DateTime(), Status = ContractStatus.CURRENT };
            Contract2 = new ActemiumContract() { ContractId = 2, Company = Google, ContractType = ContractType1, EndDate = new DateTime().AddYears(2), StartDate = new DateTime(), Status = ContractStatus.CANCELLED };
            Contract3 = new ActemiumContract() { ContractId = 3, Company = Amazon, ContractType = ContractType2, EndDate = new DateTime().AddYears(3), StartDate = new DateTime(), Status = ContractStatus.EXPIRED };
            Contract4 = new ActemiumContract() { ContractId = 1, Company = Google, ContractType = ContractType1, EndDate = new DateTime().AddYears(1), StartDate = new DateTime(), Status = ContractStatus.IN_REQUEST };

            Contracts = new[] { Contract1, Contract2, Contract3 };

            ContractTypes = new[] { ContractType1, ContractType2 };
        }
        
    }
}