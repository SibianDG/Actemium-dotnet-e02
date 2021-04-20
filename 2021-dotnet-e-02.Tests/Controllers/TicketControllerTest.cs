using System.Collections.Generic;
using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace _2021_dotnet_e_02.Tests.Controllers
{
    public class TicketControllerTest
    {
        private readonly TicketController _controller;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<ITicketRepository> _ticketRepository;

        public TicketControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _ticketRepository = new Mock<ITicketRepository>();
            _controller = new TicketController(_ticketRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        #region -- Index --

        [Fact]
        public void Index_PassesListOfTicketsOrderedByPrioInViewResult()
        {
            _ticketRepository.Setup(t => t.GetAll()).Returns(_dummyContext.Tickets);
            var result = Assert.IsType<ViewResult>(_controller.Index());
            var ticketsInModel = Assert.IsType<List<ActemiumTicket>>(result.Model);
            Assert.Equal(5, ticketsInModel.Count);
            Assert.Equal("Title", ticketsInModel[0].Title);
            Assert.Equal("Title4", ticketsInModel[1].Title);
            Assert.Equal("Title2", ticketsInModel[2].Title);
            Assert.Equal("Title5", ticketsInModel[3].Title);
            Assert.Equal("Title3", ticketsInModel[4].Title);
        }

        #endregion
    }
}