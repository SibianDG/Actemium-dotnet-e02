using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Models.ViewModels.TicketViewModel;
using _2021_dotnet_e_02.Data;
using _2021_dotnet_e_02.Data.Repositories;

namespace _2021_dotnet_e_02.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICompanyRepository _companyRepository;

        public TicketController(ITicketRepository ticketRepository, ICompanyRepository companyRepository)
        {
            _ticketRepository = ticketRepository;
            _companyRepository = companyRepository;
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
            {
                Console.WriteLine("ticket is null => not found");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("01 edit");
                    if (ticket.Status != TicketStatus.COMPLETED)
                    {
                        ticket.EditTicket(editViewModel.Priority, editViewModel.Title.Trim()
                            , editViewModel.Description.Trim(), editViewModel.Attachments, editViewModel.TicketType);
                    } else
                    {
                        Console.WriteLine(editViewModel.Solution ?? "");
                        ticket.EditTicketCompleted(editViewModel.Priority, editViewModel.Title.Trim()
                            , editViewModel.Description.Trim(), editViewModel.Attachments, editViewModel.TicketType
                            // Solution/Quality/SupportNeeded are optional values
                            //, editViewModel.Solution ?? "", editViewModel.Quality ?? "", editViewModel.SupportNeeded ?? ""); 
                            // the above method works but then we don't Trim()
                            , editViewModel.Solution != null ? editViewModel.Solution.Trim() : ""
                            , editViewModel.Quality != null ? editViewModel.Quality.Trim() : ""
                            , editViewModel.SupportNeeded != null ? editViewModel.SupportNeeded.Trim() : "");
                        Console.WriteLine("02 edit");
                    }
                    _ticketRepository.SaveChanges();
                    Console.WriteLine("03 edit");
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
                    // only for testing -> will be replaced by company of logged in user in the future
                    //CompanyRepository tempCompanyRepo = new CompanyRepository(new ApplicationDbContext());
                    ActemiumCompany company = _companyRepository.GetBy(7);

                    Console.WriteLine("01 create");
                    // only ticketstatus created can be given to new tickets created by customer
                    var ticket = new ActemiumTicket(TicketStatus.CREATED, editViewModel.Priority, editViewModel.Title
                        , company, editViewModel.Description, editViewModel.Attachments, editViewModel.TicketType);
                    Console.WriteLine("02 create");
                    Console.WriteLine(company.Name);
                    company.addActemiumTicket(ticket);
                    Console.WriteLine("company add ticket gelukt");
                    //_ticketRepository.Add(ticket);

                    Console.WriteLine("04 add ticketrepo");
                    //TODO: company meegeven
                    _companyRepository.Update(company);
                    Console.WriteLine("05 update company");
                    //_ticketRepository.Add(ticket);

                    // Code works up till here
                    // error is thrown, has to do with updating company in db fails or smth idk
                    _companyRepository.SaveChanges();
                    //_ticketRepository.SaveChanges();
                    TempData["message"] = $"You successfully added ticket {ticket.Title}.";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Sorry, something went wrong, the ticket was not added...";
                    Console.WriteLine(ex.Message);
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
