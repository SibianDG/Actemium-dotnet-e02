﻿using _2021_dotnet_e_02.Controllers;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using _2021_dotnet_e_02.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _2021_dotnet_e_02.Tests.Controllers
{
    public class KDBControllerTest
    {

        private readonly KDBController _controller;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<IKbItemRepository> _kbItemRepository;

        public KDBControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _kbItemRepository = new Mock<IKbItemRepository>();
            _controller = new KDBController(_kbItemRepository.Object);
        }

        #region -- Index GET --
        [Fact]
        public void Index_ReturnsView()
        {
            // act
            var result = Assert.IsType<ViewResult>(_controller.Index());

            // assert
            Assert.NotNull(result);
        }
        #endregion

        #region -- OverviewType GET --
        [Fact]
        public void OverviewType_PassesKBitemsToView()
        {
            // arrange
            _kbItemRepository.Setup(k => k.GetByType("hardware")).Returns(_dummyContext.KbItems);
            // act
            var result = Assert.IsType<ViewResult>(_controller.OverviewType("hardware", 1));
            var kbItemsInModel = Assert.IsType<List<ActemiumKbItem>>(result.Model);
            // assert
            Assert.Equal(2, kbItemsInModel.Count);
            Assert.Equal(KbItemType.HARDWARE, kbItemsInModel[0].Type);

        }
        #endregion
    }
}
