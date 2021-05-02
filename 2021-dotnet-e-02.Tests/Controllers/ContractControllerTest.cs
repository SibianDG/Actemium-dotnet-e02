using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Models.ViewModels.ContractViewModel;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace _2021_dotnet_e_02.Tests.Controllers
{
    public class ContractControllerTest
    {
        private readonly ContractController _controller;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<IContractRepository> _contractRepository;
        private readonly Mock<IContractTypeRepository> _contractTypeRepository;
        private readonly Mock<ICompanyRepository> _companyRepository;


        public ContractControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _contractRepository = new Mock<IContractRepository>();
            _contractTypeRepository = new Mock<IContractTypeRepository>();
            _companyRepository = new Mock<ICompanyRepository>();
            _controller = new ContractController(_contractRepository.Object, _contractTypeRepository.Object, _companyRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        #region -- Index GET --

        [Fact]
        public void Index_PassesListOfContractsOrderedByStartAndEndDateInViewResult()
        {
            //arrange
            _contractRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Contracts);

            //act
            var result = Assert.IsType<ViewResult>(_controller.Index(1));
            var contractsInModel = Assert.IsType<List<ActemiumContract>>(result.Model);

            //assert
            Assert.Equal(3, contractsInModel.Count);
            Assert.Equal("Google", contractsInModel[0].Company.Name);
            Assert.Equal("Amazon", contractsInModel[2].Company.Name);
            Assert.Equal(ContractStatus.CANCELLED, contractsInModel[1].Status);

        }

        #endregion

        #region -- Create GET--

        [Fact]
        public void Create_PassesEmptyContractCreateViewModelAndContractTypeSelectListToView()
        {
            _contractTypeRepository.Setup(ct => ct.GetAll()).Returns(_dummyContext.ContractTypes);
            // act
            var result = Assert.IsType<ViewResult>(_controller.Create());

            // assert
            var contractCreateViewModel = Assert.IsType<ContractCreateViewModel>(result.Model);
            var contractTypesSelectList = Assert.IsType<SelectList>(result.ViewData["ContractTypes"]);
            Assert.Equal(DateTime.Today, contractCreateViewModel.StartDate);
            Assert.NotNull(contractTypesSelectList);
            _contractTypeRepository.Verify(ct => ct.GetAll(), Times.Once);

        }
        #endregion

        #region -- Create POST --

        [Fact]
        public void Create_ValidTicket_CreatesAndPersistsTicket()
        {
            //Failes because there is an error in the code

            // arrange
            _contractRepository.Setup(c => c.Add(_dummyContext.Contract4));
            _contractTypeRepository.Setup(ct => ct.GetBy(1)).Returns(_dummyContext.ContractType1);
            ContractCreateViewModel contractCreateViewModel = new ContractCreateViewModel();

            // act
            var result = Assert.IsType<RedirectToActionResult>(_controller.Create(contractCreateViewModel));

            // assert
            Assert.Equal(nameof(Index), result.ActionName);
            _contractRepository.Verify(c => c.Add(_dummyContext.Contract4), Times.Once);
            _contractRepository.Verify(c => c.SaveChanges(), Times.Once);
            _contractTypeRepository.Verify(c => c.GetBy(1), Times.Once);
        }

        [Fact]
        public void Create_InvalidTicket_ThrowsExceptionAndDisplaysErrorMessage()
        {
            //TODO
        }

        #endregion
    }
}
