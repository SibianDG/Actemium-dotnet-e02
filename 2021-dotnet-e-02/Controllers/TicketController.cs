using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models.ViewModels.TicketViewModel;

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
        
        public IActionResult Edit(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            ViewData["IsEdit"] = true;
            return View(new EditViewModel(ticket));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel editViewModel)
        {
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.EditTicket(editViewModel.Status, editViewModel.Priority, editViewModel.Title
                        , editViewModel.Description, editViewModel.Attachments, editViewModel.TicketType
                        , editViewModel.Solution, editViewModel.Quality, editViewModel.SupportNeeded);
                    _ticketRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated product {ticket.Title}.";
                }
                catch
                {
                    TempData["error"] = "Sorry, something went wrong, product was not updated...";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IsEdit"] = true;
            return View(editViewModel);

        }
        
        
    }
}
