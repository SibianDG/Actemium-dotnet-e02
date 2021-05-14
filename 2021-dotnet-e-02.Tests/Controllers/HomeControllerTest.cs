using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Identity;
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
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<UserManager<IdentityUser>> _userManager;

        public HomeControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _ticketRepository = new Mock<ITicketRepository>();
            _userRepository = new Mock<IUserRepository>();
            _userManager = new Mock<UserManager<IdentityUser>>();
            _controller = new HomeController(_ticketRepository.Object, _userRepository.Object, _userManager.Object);
        }

        #region Index

        [Fact]
        public void Index_PassesListOfTicketsInViewResultModelAndStoresTotalOpenAndResolvedTicketsInViewData()
        {
            //Arange
            /*var identityUserExists = await _userManager.FindByEmailAsync("sup123@hogent.be");

            var result = await _signInManager.PasswordSignInAsync(, "sup123", "Passwd123&");*/

            _ticketRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Tickets.ToList());
            _ticketRepository.Setup(m => m.GetAllOpenTickets()).Returns(_dummyContext.OpenTickets);
            _ticketRepository.Setup(m => m.GetAllResolvedTickets()).Returns(_dummyContext.ResolvedTickets);

            //Act
            var result = Assert.IsType<ViewResult>(_controller.Index(1));

            //Assert
            var ticketsInModel = Assert.IsType<List<ActemiumTicket>>(result.Model);
            Assert.Equal(5, ticketsInModel.Count);
            Assert.Equal("Title", ticketsInModel[0].Title);
            Assert.Equal("Title2", ticketsInModel[1].Title);
            Assert.Equal(TicketStatus.IN_DEVELOPMENT, ticketsInModel[2].Status);
            Assert.Equal(3, result.ViewData["OpenTickets"]);
            Assert.Equal(1, result.ViewData["ResolvedTickets"]);
        }

        #endregion
    }
}
