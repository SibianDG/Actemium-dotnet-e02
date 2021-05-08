using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data.Repositories;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace _2021_dotnet_e_02.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IKbItemRepository _kbItemRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public StatisticsController(IKbItemRepository kbItemRepository, ICompanyRepository comRepository, IContractTypeRepository contractTypeRepository
            , IContractRepository contractRepository, ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _kbItemRepository = kbItemRepository;
            _companyRepository = comRepository;
            _contractTypeRepository = contractTypeRepository;
            _contractRepository = contractRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<ActemiumTicket> tickets = _ticketRepository.GetAll();
            Dictionary<string, int> ticketsPerStatus = new Dictionary<string, int>();
            Dictionary<string, int> frequency = tickets.GroupBy(x => x.Status.ToString()).ToDictionary(x => x.Key, x => x.Count());
            var json = JsonConvert.SerializeObject(frequency);
            ViewData["numbers"] = json;
            ViewData["cats"] = JsonConvert.SerializeObject(frequency.Keys.ToList());
            
            return View();
        }
        public JsonResult Graph1()
        {
            IEnumerable<ActemiumTicket> tickets = _ticketRepository.GetAll();
            Dictionary<string, double> ticketsPerStatus = new Dictionary<string, double>();
            Dictionary<string, double> frequency = tickets.GroupBy(x => x.Status.ToString()).ToDictionary(x => x.Key, x => (double)x.Count());
            Graph graph1 = new Graph(frequency, frequency.Keys.ToList());
            return Json(JsonConvert.SerializeObject(graph1, Formatting.Indented));
        }
        
        public JsonResult Graph2()
        {
            IEnumerable<ActemiumTicket> tickets = _ticketRepository.GetAllResolvedTickets();
            
            Dictionary<string, int> numberOfTicketsPerPrio = tickets
                .GroupBy(x => x.Priority.ToString())
                .ToDictionary(x => x.Key, x => x.Count());
            foreach (var s in numberOfTicketsPerPrio.Keys)
            {
                Console.WriteLine(s);
            }
            
            
            Console.WriteLine(numberOfTicketsPerPrio.ToString());

            Dictionary<string, double> timePerTicketPerPrio = new Dictionary<string, double>()
            {
                {"P1", 0},
                {"P2", 0},
                {"P3", 0}
            };

            foreach (ActemiumTicket t in tickets)
            {
                if (t.DateAndTimeOfCompletion != null)
                {
                    double min = ((DateTime) t.DateAndTimeOfCompletion).Subtract(t.DateAndTimeOfCreation).TotalMinutes;
                    Console.WriteLine("minutes: "+min);
                    timePerTicketPerPrio[t.Priority.ToString()] += min;
                }
            }

            foreach (var s in timePerTicketPerPrio.Keys)
            {
                Console.WriteLine(s +", "+timePerTicketPerPrio[s]);
            }

            foreach (var str in numberOfTicketsPerPrio.Keys)
            {
                timePerTicketPerPrio[str] = Math.Round((timePerTicketPerPrio[str] / numberOfTicketsPerPrio[str]), 2);
            }
            
            Graph graph2 = new Graph(timePerTicketPerPrio, timePerTicketPerPrio.Keys.ToList());
            return Json(JsonConvert.SerializeObject(graph2, Formatting.Indented));
        }

        public class Graph
        {
            public Dictionary<string, double> numbers { get; set; }

            public IEnumerable<string> cats { get; set; }

            public Graph(Dictionary<string, double> numbers, IEnumerable<string> cats)
            {
                this.numbers = numbers;
                this.cats = cats;
            }
        }
    }
}
