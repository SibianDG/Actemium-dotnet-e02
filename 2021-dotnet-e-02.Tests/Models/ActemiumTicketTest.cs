using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _2021_dotnet_e_02.Tests.Models
{
    public class ActemiumTicketTest
    {
        private readonly ActemiumEmployee _technician;
        private readonly ActemiumCompany _google;
        private readonly ActemiumCustomer _customer;

        public ActemiumTicketTest()
        {
            _technician = new ActemiumEmployee() {
                UserName = "jooKlein123",
                Password = "Passwd123",
                FirstName = "Joost",
                LastName = "Klein",
                Address = "Adress",
                PhoneNumber = "0470099874",
                Email = "student@student.hogent.be",
                Role = EmployeeRole.TECHNICIAN
            };

            _google = new ActemiumCompany()
            {
                Name = "Google",
                Country = "United States",
                City = "Mountain View, CA 94043",
                Address = "1600 Amphitheatre Parkway",
                PhoneNumber = "+1-650-253-0000"
            };

            _customer = new ActemiumCustomer() {
                UserName = "customer123", 
                Password = "PassWd123",
                FirstName = "John",
                LastName = "Smith",
                Company = _google
            };
        }

        // NOT OPTIMAL YET
        [Theory]
        [InlineData(TicketPriority.P1, TicketType.SOFTWARE, "Printer Broken", "Cannot print labels")]
        [InlineData(TicketPriority.P2, TicketType.SOFTWARE, "Printer Broken", "Cannot print labels")]
        [InlineData(TicketPriority.P3, TicketType.SOFTWARE, "Printer Broken", "Cannot print labels")]
        [InlineData(TicketPriority.P1, TicketType.HARDWARE, "Printer Broken", "Cannot print labels")]
        [InlineData(TicketPriority.P1, TicketType.NETWORK, "Printer Broken", "Cannot print labels")]
        [InlineData(TicketPriority.P1, TicketType.INFRASTRUCTURE, "Printer Broken", "Cannot print labels")]
        public void createActemiumTicket_ValidAttributes_DoesNotThrowException(TicketPriority priority, TicketType ticketType, string title, string description)
        {
            //TODO after changes in ActemiumTicket
            //var exception = Record.Exception(() => new ActemiumTicket(TicketStatus.CREATED, priority, title, description, "", ticketType, "", "", ""));
            //Assert.Null(exception);
        }

        //NOT OPTIMAL YET, 2 STILL FAIL
        [Theory]
        // title: not null or empty or blank, but special chars and digits are ok
        [InlineData(TicketStatus.CREATED, TicketPriority.P1, TicketType.SOFTWARE, null, "Cannot print labels")]
        [InlineData(TicketStatus.CREATED, TicketPriority.P1, TicketType.SOFTWARE, "", "Cannot print labels")]
        [InlineData(TicketStatus.CREATED, TicketPriority.P1, TicketType.SOFTWARE, "   ", "Cannot print labels")]

        // description: not null or empty or blank, but special chars and digits are ok
        [InlineData(TicketStatus.CREATED, TicketPriority.P1, TicketType.SOFTWARE, "Printer Broken", null)]
        [InlineData(TicketStatus.CREATED, TicketPriority.P1, TicketType.SOFTWARE, "Printer Broken", "")]
        [InlineData(TicketStatus.CREATED, TicketPriority.P1, TicketType.SOFTWARE, "Printer Broken", "         ")]

        public void createActemiumTicket_InValidAttributes_ThrowsIllegalArgumentException(TicketStatus ticketStatus,TicketPriority ticketPriority, TicketType ticketType,
            string title, string description)
        {
            //TODO after changes in actemiumTicket
            //Assert.Throws<ArgumentException>(() => new ActemiumTicket(ticketStatus, ticketPriority, title, description, "", ticketType, "", "", ""));
        }
    }
}
