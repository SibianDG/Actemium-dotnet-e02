using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
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


        public ContractControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _contractRepository = new Mock<IContractRepository>();
            _controller = new ContractController(_contractRepository.Object, _contractTypeRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        #region Index

        [Fact]
        public void Index_PassesListOfContractsOrderedByStartAndEndDateInViewResult()
        {
            //arrange
            _contractRepository.Setup(c => c.GetAll()).Returns(_dummyContext.Contracts);

            //act
            var result = Assert.IsType<ViewResult>(_controller.Index());
            var contractsInModel = Assert.IsType<List<ActemiumContract>>(result.Model);

            //assert
            Assert.Equal(3, contractsInModel.Count);
            Assert.Equal("Google", contractsInModel[0].Company.Name);
            Assert.Equal("Amazon", contractsInModel[2].Company.Name);
            Assert.Equal("CANCELLED", contractsInModel[1].Status);

        }

        #endregion

    }
}
