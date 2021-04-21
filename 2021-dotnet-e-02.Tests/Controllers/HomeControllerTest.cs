using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _2021_dotnet_e_02.Tests.Controllers
{
    public class HomeControllerTest
    {
        private HomeController _controller;
        private Mock<ITicketRepository> _ticketRepository;
        private DummyApplicationDbContext _dummyContext;

        public HomeControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _ticketRepository = new Mock<ITicketRepository>();
            _controller = new HomeController(_ticketRepository.Object);
        }

        #region Index

        [Fact]
        public void Index_PassesListOfTicketsInViewResultModelAndStoresTotalOpenAndResolvedTicketsInViewData()
        {
            //Arange
            _ticketRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Tickets.ToList());
            _ticketRepository.Setup(m => m.GetAllOpenTickets()).Returns(_dummyContext.OpenTickets);
            _ticketRepository.Setup(m => m.GetAllResolvedTickets()).Returns(_dummyContext.ResolvedTickets);

            //Act
            var result = Assert.IsType<ViewResult>(_controller.Index());

            //Assert
            var ticketsInModel = Assert.IsType<List<ActemiumTicket>>(result.Model);
            Assert.Equal(5, ticketsInModel.Count);
            Assert.Equal("Title", ticketsInModel[0].Title);
            Assert.Equal("Title2", ticketsInModel[1].Title);
            Assert.Equal(TicketStatus.IN_DEVELOPMENT, ticketsInModel[2].Status);
            Assert.Equal(3, result.ViewData["OpenTickets"]);
            Assert.Equal(2, result.ViewData["ResolvedTickets"]);
        }

        #endregion
    }
}
