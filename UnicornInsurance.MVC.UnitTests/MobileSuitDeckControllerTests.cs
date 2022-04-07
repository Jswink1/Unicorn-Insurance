using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Controllers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Models.ViewModels;
using UnicornInsurance.MVC.UnitTests.Mocks;
using Xunit;

namespace UnicornInsurance.MVC.UnitTests
{
    public class MobileSuitDeckControllerTests
    {
        private Mock<IUserItemService> _userItemService;
        private MobileSuitDeckController _mobileSuitDeckController;

        public MobileSuitDeckControllerTests()
        {
            _userItemService = MockUserItemService.GetUserItemService();

            _mobileSuitDeckController = new MobileSuitDeckController(_userItemService.Object);
        }

        [Fact]
        public async Task MobileSuitDeckIndexPageTest()
        {
            var result = await _mobileSuitDeckController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<List<UserMobileSuit>>(viewResult.ViewData.Model);

            // GetAllUserMobileSuits() should only be called once
            _userItemService.Verify(x => x.GetAllUserMobileSuits(), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task MobileSuitDeckDetailsPageTest(int userMobileSuitId)
        {
            var result = await _mobileSuitDeckController.Details(userMobileSuitId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<UserMobileSuit>(viewResult.ViewData.Model);

            model.AvailableWeaponsList.ShouldBeOfType<SelectList>();
            model.AvailableWeaponsList.Count().ShouldBe(model.AvailableWeapons.Count);

            // GetUserMobileSuitDetails() should only be called once
            _userItemService.Verify(x => x.GetUserMobileSuitDetails(userMobileSuitId), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task EquipWeaponTest(int userMobileSuitId)
        {
            var userMobileSuit = await _userItemService.Object.GetUserMobileSuitDetails(userMobileSuitId);

            var result = await _mobileSuitDeckController.Details(userMobileSuit);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe("MobileSuitDeck");
            redirectToActionResult.ActionName.ShouldBe("Details");
            redirectToActionResult.RouteValues.FirstOrDefault().Value.ShouldBe(userMobileSuitId);

            // EquipWeapon() should only be called once
            _userItemService.Verify(x => x.EquipWeapon(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task UnequipWeaponTest(int userMobileSuitId)
        {
            var result = await _mobileSuitDeckController.UnequipWeapon(userMobileSuitId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe("MobileSuitDeck");
            redirectToActionResult.ActionName.ShouldBe("Details");
            redirectToActionResult.RouteValues.FirstOrDefault().Value.ShouldBe(userMobileSuitId);

            // UnequipWeapon() should only be called once
            _userItemService.Verify(x => x.UnequipWeapon(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task InsurancePageTest(int userMobileSuitId)
        {
            var result = await _mobileSuitDeckController.Insurance(userMobileSuitId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<InsuranceVM>(viewResult.ViewData.Model);

            model.UserMobileSuit.Id.ShouldBe(userMobileSuitId);

            // GetUserMobileSuitDetails() should only be called once
            _userItemService.Verify(x => x.GetUserMobileSuitDetails(userMobileSuitId), Times.Once);
        }

        [Theory]
        [InlineData(1, SD.StandardInsurancePlan)]
        [InlineData(1, SD.SuperInsurancePlan)]
        [InlineData(2, SD.UltraInsurancePlan)]
        public async Task PurchaseInsuranceTest(int userMobileSuitId, string selectedInsurance)
        {
            InsuranceVM viewModel = new()
            {
                UserMobileSuit = new UserMobileSuit() { Id = userMobileSuitId },
                SelectedInsurance = selectedInsurance
            };

            var result = await _mobileSuitDeckController.Insurance(viewModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe("MobileSuitDeck");
            redirectToActionResult.ActionName.ShouldBe("Details");
            redirectToActionResult.RouteValues.FirstOrDefault().Value.ShouldBe(userMobileSuitId);

            // PurchaseInsurance() should only be called once
            _userItemService.Verify(x => x.PurchaseInsurance(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteMobileSuitTest(int userMobileSuitId)
        {
            var result = await _mobileSuitDeckController.Delete(userMobileSuitId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // DeleteUserMobileSuit() should only be called once
            _userItemService.Verify(x => x.DeleteUserMobileSuit(It.IsAny<int>()), Times.Once);
        }
    }
}
