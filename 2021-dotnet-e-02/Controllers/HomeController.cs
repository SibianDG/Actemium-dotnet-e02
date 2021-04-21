using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ITicketRepository _ticketRepository;

        public HomeController(/*ILogger<HomeController> logger,*/ ITicketRepository ticketRepository)
        {
            //_logger = logger;
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<ActemiumTicket> allTickets = _ticketRepository.GetAll();
            IEnumerable<ActemiumTicket> openTickets = _ticketRepository.GetAllOpenTickets();
            IEnumerable<ActemiumTicket> resolvedTickets = _ticketRepository.GetAllResolvedTickets();

            ViewData["OpentTickets"] = openTickets.ToList().Count();
            ViewData["ResolvedTickets"] = resolvedTickets.ToList().Count();

            return View(allTickets.ToList());
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
    }
}
