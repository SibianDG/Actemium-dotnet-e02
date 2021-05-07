using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2021_dotnet_e_02.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(/*ILogger<HomeController> logger,*/ 
                                ITicketRepository ticketRepository,
                                IUserRepository userRepository,
                                UserManager<IdentityUser> userManager)
        {
            //_logger = logger;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public IActionResult Index(int? page)
        {
            page ??= 1;
            page = page == 0 ? 1 : page;

            Console.WriteLine("page!!: " + page);

            //IEnumerable<ActemiumTicket> allTickets;
            IEnumerable<ActemiumTicket> openTickets;
            IEnumerable<ActemiumTicket> resolvedTickets;
            if (GetIsSupportManager())
            {
                //allTickets = _ticketRepository.GetAll();
                openTickets = _ticketRepository.GetAllOpenTickets();
                resolvedTickets = _ticketRepository.GetAllResolvedTickets();
            }
            else
            {
                //allTickets = _ticketRepository.GetAll(GetSignedInActemiumCustomer().Company);
                openTickets = _ticketRepository.GetAllOpenTickets(GetSignedInActemiumCustomer().Company);
                resolvedTickets = _ticketRepository.GetAllResolvedTickets(GetSignedInActemiumCustomer().Company);
            }

            resolvedTickets = resolvedTickets.ToList()
                .Where(t => t.DateAndTimeOfCompletion.Value.AddDays(5) >= DateTime.Now).Take(10);

            ViewData["OpenTicketCount"] = openTickets.ToList().Count();
            ViewData["ResolvedTicketCount"] = resolvedTickets.ToList().Count();

            int totalPages = openTickets.Count() / 10;
            if (openTickets.Count() % 10 != 0)
                totalPages++;
            ViewData["totalPages"] = totalPages;
            openTickets = openTickets.Skip((page.Value - 1) * 10).Take(10);
            ViewData["page"] = page;

            ViewData["ResolvedTickets"] = resolvedTickets;
            //TODO: should all tickets be passed??
            return View(openTickets.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // duplicate methods => also in TicketController
        // is there an easy fix to get these methods to some other class?
        private UserModel GetSignedInUserModel()
        {
            return _userRepository.GetByUsername(_userManager.GetUserName(User));
        }
        private ActemiumCustomer GetSignedInActemiumCustomer()
        {
            return _userRepository.GetCustomerByUsername(_userManager.GetUserName(User));
        }
        private Boolean GetIsSupportManager()
        {
            // we're not doing a full check like we do in ticketcontroller
            // and like we do in Login => because once it got checked in Login
            // we know that if a signed in user is an ActemiumEmployee
            // it can only be a support manager
            return GetSignedInUserModel() is ActemiumEmployee;
        }
    }
}
