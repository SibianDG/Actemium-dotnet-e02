using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace _2021_dotnet_e_02.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ITicketRepository _ticketRepository;

        public HomeController(/*ILogger<HomeController> logger,*/ ITicketRepository ticketRepository)
        {
            //_logger = logger;
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index(int? page)
        {
            page ??= 1;
            page = page == 0 ? 1 : page;

            Console.WriteLine("page!!: "+page);
            
            IEnumerable<ActemiumTicket> allTickets = _ticketRepository.GetAll();
            IEnumerable<ActemiumTicket> openTickets = _ticketRepository.GetAllOpenTickets();
            IEnumerable<ActemiumTicket> resolvedTickets = _ticketRepository.GetAllResolvedTickets();

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
    }
}
