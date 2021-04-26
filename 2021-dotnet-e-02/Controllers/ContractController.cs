using System;
using System.Collections.Generic;
using System.Linq;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.ViewModels.ContractViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2021_dotnet_e_02.Controllers
{
    public class ContractController : Controller
    {

        private readonly IContractRepository _contractRepository;
        private readonly IContractTypeRepository _contractTypeRepository;
        
        public ContractController(IContractRepository contractRepository, IContractTypeRepository contractTypeRepository)
        {
            _contractRepository = contractRepository;
            _contractTypeRepository = contractTypeRepository;
        }

        #region Index
        public IActionResult Index()
        {
            IEnumerable<ActemiumContract> contracts;
            //TODO performace?? this is good i think
            contracts = _contractRepository.GetAll();
            contracts = contracts.OrderBy(c => c.StartDate).ThenBy(c => c.EndDate).ToList();
            Console.WriteLine("NUMBER" + contracts.Count());
            Console.WriteLine("COMPANYNAME CONTRACT 1" + contracts.First().Company.Name);
            return View(contracts);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewData["ContractTypes"] = GetContractTypesAsSelectList();
            return View(new ContractCreateViewModel() { StartDate = DateTime.Today });
        }

        [HttpPost] //BASIC IMPLEMENTATION DOES NOT FULLY WORK YET
        public IActionResult Create(ContractCreateViewModel createViewModel)
        {
            ActemiumContract contract = new ActemiumContract();
            MapCreateViewModelToContract(createViewModel, contract);
            _contractRepository.Add(contract);
            _contractRepository.SaveChanges();
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
            contract.ContractType = contractCreateViewModel.ContractType == null ? null : _contractTypeRepository.GetBy(contractCreateViewModel.ContractType.ContractTypeId);
            contract.Status = contractCreateViewModel.Status;
            contract.StartDate = contractCreateViewModel.StartDate;
            contract.EndDate = contractCreateViewModel.StartDate.AddYears(contractCreateViewModel.Duration);
        }
    }
}