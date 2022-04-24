using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Contracts.Helpers;
using UnicornInsurance.MVC.Controllers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Models.ViewModels;
using UnicornInsurance.MVC.Services.Base;
using UnicornInsurance.MVC.UnitTests.Mocks;
using Xunit;

namespace UnicornInsurance.MVC.UnitTests
{
    public class MobileSuitsControllerTests
    {
        private Mock<IMobileSuitService> _mobileSuitService;
        private Mock<IWeaponService> _weaponService;
        private Mock<IWebHostEnvironment> _webHostEnvironment;
        private Mock<IShoppingCartService> _shoppingCartService;
        private Mock<IHttpContextHelper> _httpContextHelper;
        private Mock<IBlobService> _blobService;
        private MobileSuitsController _mobileSuitsController;
        private MobileSuitUpsertVM _mobileSuitUpsertVM;

        public MobileSuitsControllerTests()
        {
            _mobileSuitService = MockMobileSuitService.GetMobileSuitService();
            _weaponService = new Mock<IWeaponService>();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _shoppingCartService = new Mock<IShoppingCartService>();
            _httpContextHelper = new Mock<IHttpContextHelper>();
            _blobService = new Mock<IBlobService>();

            _webHostEnvironment.Setup(x => x.WebRootPath).Returns("wwwroot");

            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());

            _mobileSuitsController = new MobileSuitsController(_mobileSuitService.Object,
                                                               _weaponService.Object,
                                                               _webHostEnvironment.Object,
                                                               _shoppingCartService.Object,
                                                               _httpContextHelper.Object,
                                                               _blobService.Object)
            {
                TempData = tempData
            };

            _mobileSuitUpsertVM = new MobileSuitUpsertVM()
            {
                MobileSuit = new MobileSuit()
                {
                    Name = "New Mobile Suit",
                    Description = "Very New",
                    Price = 25000m,
                    CustomWeapon = new CustomWeapon()
                }
            };
        }

        [Theory]
        [InlineData(1,  null, 3)]
        [InlineData(1,  "uni", 1)]
        [InlineData(1,  "0", 1)]
        [InlineData(1,  "gun", 3)]
        public async Task MobileSuitsIndexPageTest(int page, string search, int expectedCount)
        {
            var result = await _mobileSuitsController.Index(page, search);            

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<MobileSuitListVM>(viewResult.ViewData.Model);

            model.MobileSuits.Count.ShouldBe(expectedCount);

            // GetMobileSuits() should only be called once
            _mobileSuitService.Verify(x => x.GetMobileSuits(), Times.Once);
        }

        [Theory]
        [InlineData(1, "Unicorn Gundam")]
        [InlineData(2, "00 Gundam")]
        [InlineData(3, "RX-78 Gundam")]
        public async Task MobileSuitDetailsPageTest(int mobileSuitId, string mobileSuitName)
        {
            var result = await _mobileSuitsController.Details(mobileSuitId);            

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<MobileSuit>(viewResult.ViewData.Model);

            model.Id.ShouldBe(mobileSuitId);
            model.Name.ShouldBe(mobileSuitName);

            // GetMobileSuitDetails() should only be called once
            _mobileSuitService.Verify(x => x.GetMobileSuitDetails(mobileSuitId), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task AddToCartTest(int mobileSuitId)
        {
            _shoppingCartService.Setup(x => x.AddMobileSuitCartItem(It.IsAny<int>()))
                                .ReturnsAsync(new BaseCommandResponse()
                                {
                                    Success = true
                                });

            var mobileSuit = await _mobileSuitService.Object.GetMobileSuitDetails(mobileSuitId);

            var result = await _mobileSuitsController.Details(mobileSuit);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // AddMobileSuitCartItem() should only be called once
            _shoppingCartService.Verify(x => x.AddMobileSuitCartItem(mobileSuitId), Times.Once);
        }

        [Theory]
        [InlineData(1, "Unicorn Gundam")]
        [InlineData(2, "00 Gundam")]
        [InlineData(3, "RX-78 Gundam")]
        public async Task UpsertPage_ShouldDisplay_UpdateMobileSuitPage(int mobileSuitId, string mobileSuitName)
        {
            var result = await _mobileSuitsController.Upsert(mobileSuitId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<MobileSuitUpsertVM>(viewResult.ViewData.Model);

            model.MobileSuit.Id.ShouldBe(mobileSuitId);
            model.MobileSuit.Name.ShouldBe(mobileSuitName);

            // GetMobileSuitDetails() should only be called once
            _mobileSuitService.Verify(x => x.GetMobileSuitDetails(mobileSuitId), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        public async Task UpsertPage_ShouldDisplay_CreateNewMobileSuitPage(int? mobileSuitId)
        {
            var result = await _mobileSuitsController.Upsert(mobileSuitId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<MobileSuitUpsertVM>(viewResult.ViewData.Model);

            model.MobileSuit.Id.ShouldBe(0);
            model.MobileSuit.Name.ShouldBeNull();

            // GetMobileSuitDetails() should not be called
            _mobileSuitService.Verify(x => x.GetMobileSuitDetails(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpsertPage_Update_Test(int mobileSuitId)
        {
            _httpContextHelper.Setup(x => x.GetUploadedFiles(It.IsAny<ControllerBase>()))
                                .Returns(new FormFileCollection());

            _mobileSuitUpsertVM.MobileSuit.Id = mobileSuitId;

            var result = await _mobileSuitsController.Upsert(_mobileSuitUpsertVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // UpdateMobileSuit() should only be called
            _mobileSuitService.Verify(x => x.UpdateMobileSuit(It.IsAny<MobileSuit>()), Times.Once);
            _mobileSuitService.Verify(x => x.InsertMobileSuit(It.IsAny<MobileSuit>()), Times.Never);
        }

        [Fact]
        public async Task UpsertPage_Create_Test()
        {
            _httpContextHelper.Setup(x => x.GetUploadedFiles(It.IsAny<ControllerBase>()))
                                .Returns(new FormFileCollection()
                                {
                                    new FormFile(It.IsAny<Stream>(), 
                                                 It.IsAny<long>(),
                                                 It.IsAny<long>(),
                                                 It.IsAny<string>(),
                                                 "image.webp")
                                });

            var result = await _mobileSuitsController.Upsert(_mobileSuitUpsertVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // InsertMobileSuit() should only be called
            _mobileSuitService.Verify(x => x.UpdateMobileSuit(It.IsAny<MobileSuit>()), Times.Never);
            _mobileSuitService.Verify(x => x.InsertMobileSuit(It.IsAny<MobileSuit>()), Times.Once);
        }

        [Fact]
        public async Task UpsertPage_ReturnsViewResult_WhenModelIsInvalid()
        {
            _mobileSuitsController.ModelState.AddModelError("Name", "Required");

            var result = await _mobileSuitsController.Upsert(_mobileSuitUpsertVM);

            var viewResult = Assert.IsType<ViewResult>(result);

            // UpdateWeapon() and InsertWeapon() should not be called
            _mobileSuitService.Verify(x => x.UpdateMobileSuit(It.IsAny<MobileSuit>()), Times.Never);
            _mobileSuitService.Verify(x => x.InsertMobileSuit(It.IsAny<MobileSuit>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DeleteMobileSuitTest(int mobileSuitId)
        {
            var result = await _mobileSuitsController.Delete(mobileSuitId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // UpdateMobileSuit() should only be called
            _mobileSuitService.Verify(x => x.DeleteMobileSuit(It.IsAny<int>()), Times.Once);
        }
    }
}
