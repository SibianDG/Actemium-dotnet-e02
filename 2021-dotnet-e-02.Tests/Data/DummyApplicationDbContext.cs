using System;
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

        public ActemiumTicket Ticket1 { get; } 
        public ActemiumTicket Ticket2 { get; }
        public ActemiumTicket Ticket3 { get; }
        public ActemiumTicket Ticket4 { get; }
        public ActemiumTicket Ticket5 { get; }
        
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
        }
        
    }
}