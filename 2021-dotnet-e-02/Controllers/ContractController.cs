using System;
using System.Collections.Generic;
using System.Linq;
using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2021_dotnet_e_02.Controllers
{
    public class ContractController : Controller
    {

        private readonly IContractRepository _contractRepository;
        
        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        
        public IActionResult Index()
        {
            IEnumerable<ActemiumContract> contracts;
            //TODO performace??
            contracts = _contractRepository.GetAll();
            contracts = contracts.OrderBy(c => c.StartDate).ThenBy(c => c.EndDate).ToList();
            Console.WriteLine("NUMBER" + contracts.Count());
            return View(contracts);
        }
    }
}