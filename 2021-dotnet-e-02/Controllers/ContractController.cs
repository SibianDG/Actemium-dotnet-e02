using System;
using System.Collections.Generic;
using System.Linq;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Models.ViewModels.ContractViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace _2021_dotnet_e_02.Controllers
{
    public class ContractController : Controller
    {

        private readonly IContractRepository _contractRepository;
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly ICompanyRepository _companyRepository;
        
        public ContractController(IContractRepository contractRepository, IContractTypeRepository contractTypeRepository, ICompanyRepository companyRepository)
        {
            _contractRepository = contractRepository;
            _contractTypeRepository = contractTypeRepository;
            _companyRepository = companyRepository;
        }

        #region Index
        public IActionResult Index(int? page, string searchText = null, DateTime? startDate = null, DateTime? endDate = null, List<int> status = null)
        {
            page ??= 1;
            page = page == 0 ? 1 : page;
            
            IEnumerable<ActemiumContract> contracts;
            //TODO performace?? this is good i think
            contracts = _contractRepository.GetAll();
            contracts = contracts.OrderBy(c => c.StartDate).ThenBy(c => c.EndDate).ToList();

            if (searchText != null)
            {
                contracts = contracts.Where(t =>
                    t.Status.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    t.ContractType.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    );
            }
            
            if (status.Count != 0)
            {
                contracts = contracts.Where(t => status.Contains((int) t.Status));
            }
            if (startDate != null)
                contracts = contracts.Where(t => t.StartDate >= startDate);
            if (endDate != null)
                contracts = contracts.Where(t => t.EndDate <= endDate);
            
            int totalPages = contracts.Count() / 10;
            if (contracts.Count() % 10 != 0)
                totalPages++;
            ViewData["totalPages"] = totalPages;
            
            contracts = contracts.Skip((page.Value - 1) * 10).Take(10);
            ViewData["page"] = page;
            
            ViewData["SearchText"] = searchText;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            ViewData["status"] = JsonConvert.SerializeObject(status);
            
            return View(contracts);
        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int id)
        {
            //TODO: When he makes a JSON, it will go to ex. comment to make those a JSON, but comment has an association to the same ticket --> Cycle
            ActemiumContract contract = _contractRepository.GetBy(id);
            if (contract == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(contract);
            Console.WriteLine(json);
            Console.WriteLine("jsonTYPE: "+ json.GetType());
            
            return Json(json); 
        }

        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewData["ContractTypes"] = GetContractTypesAsSelectList();
            return View(new ContractCreateViewModel() { StartDate = DateTime.Today });
        }

        [HttpPost] 
        public IActionResult Create(ContractCreateViewModel createViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ActemiumContract contract = new ActemiumContract();
                    MapCreateViewModelToContract(createViewModel, contract);
                    contract.Company = _companyRepository.GetBy(3);
                    _contractRepository.Add(contract);
                    Console.WriteLine("HIER");
                    Console.WriteLine(contract.ContractId);
                    _contractRepository.SaveChanges();
                    TempData["success"] = "Succesfully signed a new contract.";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Sorry, something went wrong, the contract was not signed...";
                    Console.WriteLine(ex.Message);
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Create), createViewModel);
        }

        #endregion

        private SelectList GetContractTypesAsSelectList()
        {
            return new SelectList(
                _contractTypeRepository.GetAll().OrderBy(ct => ct.Name),
                nameof(ActemiumContractType.ContractTypeId),
                nameof(ActemiumContractType.Name)
                );
        }

        private void MapCreateViewModelToContract(ContractCreateViewModel contractCreateViewModel, ActemiumContract contract)
        {
            Console.WriteLine("contracttype id: " + contractCreateViewModel.ContractType);
            contract.ContractType = _contractTypeRepository.GetBy(contractCreateViewModel.ContractType);
            contract.Status = ContractStatus.IN_REQUEST;
            contract.StartDate = contractCreateViewModel.StartDate;
            contract.EndDate = contractCreateViewModel.StartDate.AddYears(contractCreateViewModel.Duration);
        }
    }
}