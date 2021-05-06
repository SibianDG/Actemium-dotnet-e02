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
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace _2021_dotnet_e_02.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public TicketController(ITicketRepository ticketRepository, ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index(int? page, string searchText = null, List<int> type = null, List<int> priority = null,  List<int> status = null)
        {

            Console.WriteLine("PAGE first: "+page);
            page ??= 1;
            page = page == 0 ? 1 : page;
            Console.WriteLine("PAGE2: "+page);


            IEnumerable<ActemiumTicket> tickets;
            //TODO performance??
            tickets = _ticketRepository.GetAll();

            tickets = tickets.OrderBy(t => t.Priority).ThenBy(t => t.DateAndTimeOfCreation).ToList();
            
            if (searchText != null)
            {
                tickets = tickets.Where(t => t.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                             t.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                             t.Priority.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                             t.TicketType.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                             t.Status.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)
                );
            }
            
            if (type.Count != 0)
                tickets = tickets.Where(t => type.Contains((int)t.TicketType));
            if (priority.Count != 0)
                tickets = tickets.Where(t => priority.Contains((int)t.Priority));
            if (status.Count != 0)
                tickets = tickets.Where(t => status.Contains((int)t.Status));
            
            int totalPages = tickets.Count() / 10;
            if (tickets.Count() % 10 != 0)
                totalPages++;
            ViewData["totalPages"] = totalPages;

            Console.WriteLine("PAGE value: "+(page.Value));
            Console.WriteLine("PAGE skip: "+((page.Value - 1) * 10));
            tickets = tickets.Skip((page.Value - 1) * 10).Take(10);

            ViewData["SearchText"] = searchText;
            ViewData["page"] = page;
            //TODO: you should know what type you selected...
            ViewData["status"] = GetTicketStatusSelectList();

            ViewData["types"] = JsonConvert.SerializeObject(type);
            ViewData["priority"] = JsonConvert.SerializeObject(priority);
            ViewData["status"] = JsonConvert.SerializeObject(status);
            
            Console.WriteLine(ViewData["status"]);
            return View(tickets);
        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            //TODO: When he makes a JSON, it will go to ex. comment to make those a JSON, but comment has an association to the same ticket --> Cycle
            //Temp solution with different getBy and getById methods, getById doesnt include tickets and will be used for right details pane
            Console.WriteLine("IDDDDDDDD: "+id);
            ActemiumTicket ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();
            Console.WriteLine("TICKKKETT: "+ticket.Title);
            var json = JsonConvert.SerializeObject(ticket);
            Console.WriteLine(json);
            Console.WriteLine("jsonTYPE: "+ json.GetType());
            
            return Json(json); 
        }
        
        public IActionResult DetailsNewWindow(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetBy(id);
            if (ticket == null)
                return NotFound();
            return View(ticket);
        }

        public IActionResult FullDetailsNewWindow(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetBy(id);
            if (ticket == null)
                return NotFound();
            ViewData["AddingComments"] = false;
            return View(ticket);
        }

        // Post method is only for adding a new comment
        [HttpPost]
        public IActionResult FullDetailsNewWindow(int id, ActemiumTicket editViewModel)
        {
            ActemiumTicket ticket = _ticketRepository.GetBy(id);
            if (ticket == null)
            {
                Console.WriteLine("ticket is null => not found");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO change by signed in user
                    UserModel user = _userRepository.GetBy(3);
                    string userRole = "Customer";

                    ticket.AddNewComment(ticket, user, userRole, editViewModel.NewComment);
                  
                    _ticketRepository.SaveChanges();

                    TempData["message"] = $"You successfully added a comment to the ticket {ticket.Title}.";

                    // make editViewModel newComment empty again
                    editViewModel.NewComment = "";
                    ModelState.Clear();
                }
                catch
                {
                    TempData["error"] = "Sorry, something went wrong, comment was not added to ticket...";
                }
                ViewData["AddingComments"] = true;
                return View(ticket);
                //return View(editViewModel);
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("NOT VALID");
            }
            ViewData["AddingComments"] = true;
            ViewData["IsEdit"] = true;
            return View(ticket);
        }

        public IActionResult Edit(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetBy(id);
            if (ticket == null)
                return NotFound();
            ViewData["IsEdit"] = true;
            Console.WriteLine("RETURN VIEW EDIT");
            return View(new EditViewModel(ticket));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel editViewModel)
        {
            ActemiumTicket ticket = _ticketRepository.GetBy(id);
            if (ticket == null)
            {
                Console.WriteLine("ticket is null => not found");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
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
                    }
                    _ticketRepository.SaveChanges();
                    TempData["message"] = $"You successfully updated ticket {ticket.Title}.";

                    ViewData["AddingComments"] = false;
                    return View(nameof(FullDetailsNewWindow), ticket);
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
            // Just to clarify because it was confusing
            // Will redirect to Edit.cshtml and NOT to Create.cshtml (=> not in use)
            return View(nameof(Edit), new EditViewModel());
            // But when you submit in Edit.cshtml after this method was called
            // then it will use the Post Create method below
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
                    ActemiumCompany company = _companyRepository.GetBy(3);

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

                    ViewData["AddingComments"] = false;
                    return View(nameof(FullDetailsNewWindow), ticket);
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
        public IActionResult DeleteConfirmed(int id) //TODO has to return IActionResult but does return void now because there was an error
        {
            Console.WriteLine("IN DeleteConfirmed: "+id);
            try
            {
                ActemiumTicket ticket = _ticketRepository.GetById(id);
                if (ticket == null)
                     return NotFound();
                ticket.Status = TicketStatus.CANCELLED;
                _ticketRepository.Update(ticket);
                TempData["message"] = "You successfully changed the ticket status to cancelled.";
                _ticketRepository.SaveChanges();
                Console.WriteLine("SUCCESS");
            }
            catch
            {
                TempData["error"] = "Sorry, something went wrong, the ticket status wasn't changed";
            }
            return RedirectToAction(nameof(Index));
        }
        
        private SelectList GetTicketStatusSelectList(int selected = 0)
        {
            Console.WriteLine("GetTicketStatusSelectList");
            Console.WriteLine(Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList());
            SelectList sl = new SelectList(Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList(),
                nameof(TicketStatus), nameof(TicketStatus.ToString), selected);
            return sl;       
        }
    }
    
}
