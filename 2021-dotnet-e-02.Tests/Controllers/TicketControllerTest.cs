using System;
using System.Collections.Generic;
using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Models.ViewModels.TicketViewModel;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Identity;
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
        private readonly Mock<ICompanyRepository> _companyRepository;
        private readonly Mock<IContractRepository> _contractRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<UserManager<IdentityUser>> _userManager;

        public TicketControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _dummyContext = new DummyApplicationDbContext();
            _ticketRepository = new Mock<ITicketRepository>();
            _companyRepository = new Mock<ICompanyRepository>();
            _userRepository = new Mock<IUserRepository>();
            _userManager = new Mock<UserManager<IdentityUser>>();
            _controller = new TicketController(_ticketRepository.Object, _companyRepository.Object, _userRepository.Object, _userManager.Object, _contractRepository.Object)
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
            var result = Assert.IsType<ViewResult>(_controller.Index(1));
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

        #region -- Details --
        [Fact] // TODO
        public void Details2_PassesViewOfTicket()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var result = Assert.IsType<ViewResult>(_controller.FullDetailsNewWindow(1));
            var ticket = Assert.IsType<ActemiumTicket>(result.Model);
            Assert.Equal("Title", ticket.Title);
        }
        #endregion

        #region -- Details POST --

        [Fact] // TODO
        public void Details_returnsValidJsonOfTicket()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var result = Assert.IsType<JsonResult>(_controller.Details(1));
            var ticket = Assert.IsType<ActemiumTicket>(result.Value);
            Assert.Equal("Title", ticket.Title);
            Assert.Equal("Description", ticket.Description);
            Assert.Equal("P1", ticket.Priority.ToString());
        }
        #endregion

        #region -- Edit POST --
        //TODO extra tests + test for edit completed ticket
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

        #region -- Create GET--

        [Fact]
        public void Create_PassesNewTicketInEditViewModel()
        {
            var result = Assert.IsType<ViewResult>(_controller.Create());
            var ticketEvm = Assert.IsType<EditViewModel>(result.Model);
            Assert.Null(ticketEvm.Title);
        }
        #endregion

        #region -- Create POST --

        [Fact]
        public void Create_ValidTicket_CreatesAndPersistsTicket()
        {
            //TODO

            // arrange
            _companyRepository.Setup(c => c.GetBy(1)).Returns(_dummyContext.Amazon);
            EditViewModel editViewModel = new EditViewModel() { Priority = TicketPriority.P1, Attachments = "This are the attachments for a test ticket", Title = "Ticket test", Description = "This is the description for a test ticket", TicketType = TicketType.DATABASE };
            _companyRepository.Setup(c => c.Update(_dummyContext.Amazon));

            // act
            var result = Assert.IsType<RedirectToActionResult>(_controller.Create(editViewModel));

            // assert
            Assert.Equal(nameof(Index), result.ActionName);
            Assert.Equal($"You successfully added ticket { editViewModel.Title}.", _controller.TempData["message"]);

            //_companyRepository.Verify(c => c.Update(_dummyContext.Amazon), Times.Once);
            //_companyRepository.Verify(c => c.SaveChanges(), Times.Once);
        }
        
        [Fact]
        public void Create_InValidTicket_DoesNotCreatesNorPersistsTicket()
        {
            //TODO
        }
        #endregion

        #region -- Delete GET --
        [Fact]
        public void Delete_PassesTicketInView()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var result = Assert.IsType<ViewResult>(_controller.DeleteConfirmed(1));
            var ticket = Assert.IsType<ActemiumTicket>(result.Model);
            Assert.Equal("Title", ticket.Title);
        }
        #endregion

        #region -- Delete POST --
        [Fact]
        public void Delete_ExistingTicket_DeletesTicketAndPersistsChanges()
        {
            _ticketRepository.Setup(t => t.GetById(1)).Returns(_dummyContext.Ticket1);
            var result = Assert.IsType<RedirectToActionResult>(_controller.DeleteConfirmed(1));
            Assert.Equal("Index", result.ActionName);
            _ticketRepository.Verify(t => t.GetById(1), Times.Once);
            _ticketRepository.Verify(t => t.SaveChanges(), Times.Once);
        }
        #endregion
    }
}