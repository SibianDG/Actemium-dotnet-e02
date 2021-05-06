using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data.Repositories;
using _2021_dotnet_e_02.Models;
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
    }
}
