using System;
using System.Collections.Generic;
using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.ViewModels.TicketViewModel;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace _2021_dotnet_e_02.Tests.Controllers
{
    public class TicketControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TicketController _controller;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<ITicketRepository> _ticketRepository;

        public TicketControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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
            foreach (var t in _dummyContext.Tickets)
            {
                _testOutputHelper.WriteLine(t.TicketId.ToString());
            }
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

        #region -- Edit GET --
        [Fact]
        public void Edit_PassesTicketInEditViewModel()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var result = Assert.IsType<ViewResult>(_controller.Edit(1));
            var ticketEvm = Assert.IsType<EditViewModel>(result.Model);
            Assert.Equal("Title", ticketEvm.Title);
            Assert.Equal("Description", ticketEvm.Description);
        }
        #endregion

        #region -- Edit POST --

        [Fact]
        public void Edit_ValidEdit_UpdatesAndPersistsTicket()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var ticketEvm = new EditViewModel(_dummyContext.Ticket1)
            {
                Title = "Nieuwe titel"
            };
            var result = Assert.IsType<RedirectToActionResult>(_controller.Edit( 1, ticketEvm));
            var ticket = _dummyContext.Ticket1;
            Assert.Equal("Index", result?.ActionName);
            Assert.Equal("Nieuwe titel", ticket.Title);
            Assert.Equal("Description", ticketEvm.Description);
            _ticketRepository.Verify(t => t.SaveChanges(), Times.Once());
        }
        
        [Fact]
        public void Edit_InValidEdit_DoesntChangeNorPersistsTicket()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var ticketEvm = new EditViewModel(_dummyContext.Ticket1)
            {
                Title = null
            };
            var result = Assert.IsType<RedirectToActionResult>(_controller.Edit( 1, ticketEvm));
            var ticket = _dummyContext.Ticket1;
            Assert.Equal("Index", result?.ActionName);
            Assert.Equal("Title", ticket.Title);
            Assert.Equal("Description", ticketEvm.Description);
            _ticketRepository.Verify(t => t.SaveChanges(), Times.Never());
        }
        #endregion
    }
}