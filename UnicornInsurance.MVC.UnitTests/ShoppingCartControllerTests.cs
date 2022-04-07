using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Contracts.Helpers;
using UnicornInsurance.MVC.Controllers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;
using UnicornInsurance.MVC.UnitTests.Mocks;
using Xunit;

namespace UnicornInsurance.MVC.UnitTests
{
    public class ShoppingCartControllerTests
    {
        private Mock<IOrderService> _orderService;
        private Mock<IShoppingCartService> _shoppingCartService;
        private CartController _cartController;

        public ShoppingCartControllerTests()
        {
            _shoppingCartService = MockShoppingCartService.GetShopingCartService();
            _orderService = new Mock<IOrderService>();

            _cartController = new CartController(_shoppingCartService.Object,
                                                 _orderService.Object);
        }

        [Fact]
        public async Task CartIndexPageTest()
        {
            var result = await _cartController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<ShoppingCartVM>(viewResult.ViewData.Model);

            // GetAllMobileSuitCartItems() and GetAllWeaponCartItems() should only be called once
            _shoppingCartService.Verify(x => x.GetAllMobileSuitCartItems(), Times.Once);
            _shoppingCartService.Verify(x => x.GetAllWeaponCartItems(), Times.Once);
        }

        [Fact]
        public async Task IncreaseWeaponQuantityTest()
        {
            var result = await _cartController.IncreaseWeaponQuantity(It.IsAny<int>());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // IncreaseWeaponQuantity() should only be called once
            _shoppingCartService.Verify(x => x.IncreaseWeaponQuantity(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DecreaseWeaponQuantityTest()
        {
            var result = await _cartController.DecreaseWeaponQuantity(It.IsAny<int>());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // DecreaseWeaponQuantity() should only be called once
            _shoppingCartService.Verify(x => x.DecreaseWeaponQuantity(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMobileSuitItemTest()
        {
            var result = await _cartController.DeleteMobileSuitItem(It.IsAny<int>());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // DeleteMobileSuitCartItem() should only be called once
            _shoppingCartService.Verify(x => x.DeleteMobileSuitCartItem(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteWeaponItemTest()
        {
            var result = await _cartController.DeleteWeaponItem(It.IsAny<int>());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // DeleteWeaponCartItem() should only be called once
            _shoppingCartService.Verify(x => x.DeleteWeaponCartItem(It.IsAny<int>()), Times.Once);
        }
    }
}
