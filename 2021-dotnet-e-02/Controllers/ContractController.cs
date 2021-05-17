using System;
using System.Collections.Generic;
using System.Linq;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Models.ViewModels.ContractViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace _2021_dotnet_e_02.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {

        private readonly IContractRepository _contractRepository;
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ContractController(IContractRepository contractRepository,   
                                IContractTypeRepository contractTypeRepository, 
                                ICompanyRepository companyRepository,
                                IUserRepository userRepository,
                                UserManager<IdentityUser> userManager)
        {
            _contractRepository = contractRepository;
            _contractTypeRepository = contractTypeRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        /*public ContractController(IContractRepository contractRepository,
                                IContractTypeRepository contractTypeRepository,
                                ICompanyRepository companyRepository)
        {
            _contractRepository = contractRepository;
            _contractTypeRepository = contractTypeRepository;
            _companyRepository = companyRepository;
        }*/

        #region Index
        public IActionResult Index(int? page, string searchText = null, DateTime? startDate = null, DateTime? endDate = null, List<int> status = null)
        {
            page ??= 1;
            page = page == 0 ? 1 : page;
            
            IEnumerable<ActemiumContract> contracts;
            if (SetIsSupportManager())
            {
                //TODO performace?? this is good i think
                contracts = _contractRepository.GetAll();
            }
            else
            {
                contracts = _contractRepository.GetAll(GetSignedInActemiumCustomer().Company);
            }
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

            if (page.Value > totalPages)
                page = totalPages;
            if (page.Value <= 0)
                page = 1;

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
            if (ModelState.IsValid)
            {
                try
                {
                    ActemiumContract contract = new ActemiumContract();
                    MapCreateViewModelToContract(createViewModel, contract);
                    contract.Company = GetSignedInActemiumCustomer().Company;
                    _contractRepository.Add(contract);
                    _contractRepository.SaveChanges();
                    TempData["success"] = "Successfully requested a new contract.";
                    Console.WriteLine(TempData["success"]);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    TempData["error"] = "Sorry, something went wrong, the contract was not requested...";
                    Console.WriteLine(ex.Message);
                }
                return View(nameof(Create), createViewModel);
            }
            return View(nameof(Create), createViewModel);
        }

        #endregion

        #region Delete

        [HttpPost, ActionName("Cancel")]
        public IActionResult CancelConfirmed(int id)
        {
            try
            {
                ActemiumContract contract = _contractRepository.GetBy(id);
                if (contract == null)
                    return NotFound();
                contract.Status = ContractStatus.CANCELLED;
                _contractRepository.Update(contract);
                TempData["message"] = "You successfully cancelled the contract!";
                _contractRepository.SaveChanges();
                Console.WriteLine("Canceled contract");
            }
            catch
            {
                TempData["error"] = "Sorry, something went wrong, the contract wasn't cancelled";
            }

            return RedirectToAction(nameof(Index));
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

        // duplicate methods => also in TicketController
        // is there an easy fix to get these methods to some other class?
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

            // we're not doing a full check like we do in ticketcontroller
            // and like we do in Login => because once it got checked in Login
            // we know that if a signed in user is an ActemiumEmployee
            // it can only be a support manager
            if (javaUser is ActemiumEmployee)
            {
                    ViewData["IsSupportManager"] = true;
                    isSupportManager = true;
            }
            else
            {
                ViewData["IsSupportManager"] = false;
                isSupportManager = false;
            }
            return isSupportManager;
        }
    }
}