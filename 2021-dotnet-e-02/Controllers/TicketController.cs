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
using Microsoft.AspNetCore.Identity;

namespace _2021_dotnet_e_02.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketController(ITicketRepository ticketRepository, 
                                ICompanyRepository companyRepository, 
                                IUserRepository userRepository, 
                                UserManager<IdentityUser> userManager)
        {
            _ticketRepository = ticketRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _userManager = userManager;
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

        public IActionResult FullDetailsNewWindow(int id)
        {
            ActemiumTicket ticket = _ticketRepository.GetBy(id);
            if (ticket == null)
                return NotFound();
            ViewData["AddingComments"] = false;
            SetIsSupportManager();
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
                    SetIsSupportManager();

                    var javaUser = GetSignedInUserModel();

                    string javaRole;
                    if (javaUser is ActemiumEmployee)
                    {
                        javaRole = ((ActemiumEmployee)javaUser).Role == EmployeeRole.SUPPORT_MANAGER ? "SUPPORT_MANAGER" : "UserRoleError";
                    }
                    else
                    {
                        javaRole = "Customer";
                    }

                    ticket.AddNewComment(ticket, javaUser, javaRole, editViewModel.NewComment);
                  
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

            SetIsSupportManager();

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
                    SetIsSupportManager();

                    if (ticket.Status != TicketStatus.COMPLETED)
                    {
                        ticket.EditTicket(editViewModel.Status, editViewModel.Priority, editViewModel.Title.Trim()
                            , editViewModel.Description.Trim(), editViewModel.Attachments, editViewModel.TicketType);
                    } else
                    {
                        ticket.EditTicketCompleted(editViewModel.Status, editViewModel.Priority, editViewModel.Title.Trim()
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

            // sets ViewData["IsSupportManager"]
            // then it returns false when it's a customer
            // if it's a customer we set ViewData["SignedInUserCompany"]
            if (!SetIsSupportManager())
            {
                // let me know if there is a better way, but this works just fine
                ViewData["SignedInUserCompany"] = GetSignedInActemiumCustomer().Company.Name;
            }

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
                    ActemiumCompany company = null;
                    ActemiumTicket ticket = null;

                    if (SetIsSupportManager())
                    {
                        company = _companyRepository.GetByName(editViewModel.CompanyName);
                        if (company == null)
                        {
                            throw new Exception("Company name does not exist!!");
                        }
                        // supportManager can assign any ticketstatus
                        ticket = new ActemiumTicket(editViewModel.Status, editViewModel.Priority, editViewModel.Title
                            , company, editViewModel.Description, editViewModel.Attachments, editViewModel.TicketType);
                    }
                    else
                    {
                        company = GetSignedInActemiumCustomer().Company;
                        // only ticketstatus created can be given to new tickets created by customer
                        ticket = new ActemiumTicket(TicketStatus.CREATED, editViewModel.Priority, editViewModel.Title
                            , company, editViewModel.Description, editViewModel.Attachments, editViewModel.TicketType);
                    }

                    company.addActemiumTicket(ticket);

                    _companyRepository.Update(company);

                    _companyRepository.SaveChanges();

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

        private UserModel GetSignedInUserModel()
        {
            return _userRepository.GetByUsername(_userManager.GetUserName(User));
        }
        private ActemiumCustomer GetSignedInActemiumCustomer()
        {
            return _userRepository.GetCustomerByUsername(_userManager.GetUserName(User));
        }

        private Boolean SetIsSupportManager()
        {
            Boolean isSupportManager = false;
            var javaUser = GetSignedInUserModel();
            if (javaUser is ActemiumEmployee)
            {
                if (((ActemiumEmployee)javaUser).Role == EmployeeRole.SUPPORT_MANAGER)
                {
                    ViewData["IsSupportManager"] = true;
                    isSupportManager = true;
                }
            }
            else
            {
                ViewData["IsSupportManager"] = false;
                isSupportManager = false;
            }
            return isSupportManager;
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
