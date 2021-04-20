using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models.Enums;
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
            //TODO performace??
            tickets = _ticketRepository.GetAll();
            tickets = tickets.OrderBy(t => t.Priority).ThenBy(t => t.DateAndTimeOfCreation).ToList();
            Console.WriteLine("NUMBER" + tickets.Count());
            return View(tickets);
        }
        
        [HttpPost]
        public IActionResult Details(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            return Json(ticket);
        }
        
        public IActionResult Details2(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            return View(ticket);
        }
        
        public IActionResult Edit(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            ViewData["IsEdit"] = true;
            Console.WriteLine("RETURN VIEW EDIT");
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
                    ticket.EditTicket(editViewModel.Status, editViewModel.Priority, editViewModel.Title.Trim()
                        , editViewModel.Description, editViewModel.Attachments, editViewModel.TicketType
                        , editViewModel.Solution, editViewModel.Quality, editViewModel.SupportNeeded);
                    _ticketRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated ticket {ticket.Title}.";
                }
                catch
                {
                    TempData["error"] = "Sorry, something went wrong, ticket was not updated...";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("NOT VALID");
            }
            ViewData["IsEdit"] = true;
            return View(editViewModel);

        }
        
        public IActionResult Create()
        {
            ViewData["IsEdit"] = false;
            return View(nameof(Edit), new EditViewModel());
        }
        
        [HttpPost]
        public IActionResult Create(EditViewModel editViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ticket = new ActemiumTicket(editViewModel.Status, editViewModel.Priority, editViewModel.Title
                        , editViewModel.Description, editViewModel.Attachments, editViewModel.TicketType
                        , editViewModel.Solution, editViewModel.Quality, editViewModel.SupportNeeded);
                    _ticketRepository.Add(ticket);
                    //TODO: company meegeven
                    _ticketRepository.SaveChanges();
                    TempData["message"] = $"You successfully added ticket {ticket.Title}.";
                }
                catch
                {
                    TempData["error"] = "Sorry, something went wrong, the ticket was not added...";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IsEdit"] = false;
            return View(nameof(Edit), editViewModel);
        }
        
        public IActionResult Delete(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                ActemiumTicket ticket = _ticketRepository.GetById(id);
                if (ticket == null)
                    return NotFound();
                ticket.Status = TicketStatus.CANCELLED;
                _ticketRepository.Update(ticket);
                TempData["message"] = "You successfully changed the ticket status to cancelled.";
                _ticketRepository.SaveChanges();
            }
            catch
            {
                TempData["error"] = "Sorry, something went wrong, the ticket status wasn't changed";
            }
            return RedirectToAction(nameof(Index));
        }
        
        
    }
}
