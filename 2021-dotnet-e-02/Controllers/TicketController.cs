using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<ActemiumTicket> tickets;
            tickets = _ticketRepository.GetAll();
            tickets = tickets.OrderBy(t => t.Priority).ThenBy(t => t.DateAndTimeOfCreation).ToList();
            return View(tickets);
        }
    }
}
