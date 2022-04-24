using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
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
    public class WeaponsControllerTests
    {
        private Mock<IWeaponService> _weaponService;
        private Mock<IWebHostEnvironment> _webHostEnvironment;
        private Mock<IShoppingCartService> _shoppingCartService;
        private Mock<IHttpContextHelper> _httpContextHelper;
        private Mock<IBlobService> _blobService;
        private WeaponsController _weaponsController;
        private WeaponUpsertVM _weaponUpsertVM;

        public WeaponsControllerTests()
        {
            _weaponService = MockWeaponService.GetWeaponService();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _shoppingCartService = new Mock<IShoppingCartService>();
            _httpContextHelper = new Mock<IHttpContextHelper>();
            _blobService = new Mock<IBlobService>();

            _webHostEnvironment.Setup(x => x.WebRootPath).Returns("wwwroot");

            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());

            _weaponsController = new WeaponsController(_weaponService.Object,
                                                       _webHostEnvironment.Object,
                                                       _shoppingCartService.Object,
                                                       _httpContextHelper.Object,
                                                       _blobService.Object)
            {
                TempData = tempData
            };

            _weaponUpsertVM = new WeaponUpsertVM()
            {
                Weapon = new Weapon()
                {
                    Name = "New Weapon",
                    Description = "Very New",
                    Price = 2500m,
                    ImageUrl = ""
                }
            };
        }

        [Theory]
        [InlineData(1, null, 3)]
        [InlineData(1, "beam", 3)]
        [InlineData(1, "beam s", 2)]
        [InlineData(1, "saber", 1)]
        public async Task WeaponsIndexPageTest(int page, string search, int expectedCount)
        {
            var result = await _weaponsController.Index(page, search);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<WeaponListVM>(viewResult.ViewData.Model);

            model.Weapons.Count.ShouldBe(expectedCount);

            // GetWeapons() should only be called once
            _weaponService.Verify(x => x.GetWeapons(), Times.Once);
        }

        [Theory]
        [InlineData(1, "Beam Rifle")]
        [InlineData(2, "Beam Spray Gun")]
        [InlineData(3, "Beam Saber")]
        public async Task WeaponDetailsPageTest(int weaponId, string weaponName)
        {
            var result = await _weaponsController.Details(weaponId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<Weapon>(viewResult.ViewData.Model);

            model.Id.ShouldBe(weaponId);
            model.Name.ShouldBe(weaponName);

            // GetWeaponDetails() should only be called once
            _weaponService.Verify(x => x.GetWeaponDetails(weaponId), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task AddToCartTest(int weaponId)
        {
            _shoppingCartService.Setup(x => x.AddWeaponCartItem(It.IsAny<int>()))
                                .ReturnsAsync(new BaseCommandResponse()
                                {
                                    Success = true
                                });

            var mobileSuit = await _weaponService.Object.GetWeaponDetails(weaponId);

            var result = await _weaponsController.Details(mobileSuit);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // AddWeaponCartItem() should only be called once
            _shoppingCartService.Verify(x => x.AddWeaponCartItem(weaponId), Times.Once);
        }

        [Theory]
        [InlineData(1, "Beam Rifle")]
        [InlineData(2, "Beam Spray Gun")]
        [InlineData(3, "Beam Saber")]
        public async Task UpsertPage_ShouldDisplay_UpdateWeaponPage(int weaponId, string weaponName)
        {
            var result = await _weaponsController.Upsert(weaponId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<WeaponUpsertVM>(viewResult.ViewData.Model);

            model.Weapon.Id.ShouldBe(weaponId);
            model.Weapon.Name.ShouldBe(weaponName);

            // GetWeaponDetails() should only be called once
            _weaponService.Verify(x => x.GetWeaponDetails(weaponId), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        public async Task UpsertPage_ShouldDisplay_CreateNewWeaponPage(int? weaponId)
        {
            var result = await _weaponsController.Upsert(weaponId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<WeaponUpsertVM>(viewResult.ViewData.Model);

            model.Weapon.Id.ShouldBe(0);
            model.Weapon.Name.ShouldBeNull();

            // GetWeaponDetails() should not be called
            _weaponService.Verify(x => x.GetWeaponDetails(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpsertPage_Update_Test(int weaponId)
        {
            _httpContextHelper.Setup(x => x.GetUploadedFiles(It.IsAny<ControllerBase>()))
                                .Returns(new FormFileCollection());

            _weaponUpsertVM.Weapon.Id = weaponId;

            var result = await _weaponsController.Upsert(_weaponUpsertVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // UpdateWeapon() should only be called
            _weaponService.Verify(x => x.UpdateWeapon(It.IsAny<Weapon>()), Times.Once);
            _weaponService.Verify(x => x.InsertWeapon(It.IsAny<Weapon>()), Times.Never);
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

            var result = await _weaponsController.Upsert(_weaponUpsertVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // InsertMobileSuit() should only be called
            _weaponService.Verify(x => x.UpdateWeapon(It.IsAny<Weapon>()), Times.Never);
            _weaponService.Verify(x => x.InsertWeapon(It.IsAny<Weapon>()), Times.Once);
        }

        [Fact]
        public async Task UpsertPage_ReturnsViewResult_WhenModelIsInvalid()
        {
            _weaponsController.ModelState.AddModelError("Name", "Required");

            var result = await _weaponsController.Upsert(_weaponUpsertVM);

            var viewResult = Assert.IsType<ViewResult>(result);

            // UpdateWeapon() and InsertWeapon() should not be called
            _weaponService.Verify(x => x.UpdateWeapon(It.IsAny<Weapon>()), Times.Never);
            _weaponService.Verify(x => x.InsertWeapon(It.IsAny<Weapon>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DeleteWeaponTest(int weaponId)
        {
            var result = await _weaponsController.Delete(weaponId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // DeleteWeapon() should only be called
            _weaponService.Verify(x => x.DeleteWeapon(It.IsAny<int>()), Times.Once);
        }
    }
}
