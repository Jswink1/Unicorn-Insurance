using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Controllers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.UnitTests.Mocks;
using Xunit;

namespace UnicornInsurance.MVC.UnitTests
{
    public class OrdersControllerTests
    {
        private Mock<IOrderService> _orderService;
        private OrdersController _ordersController;

        public OrdersControllerTests()
        {
            _orderService = MockOrderService.GetOrderService();

            _ordersController = new OrdersController(_orderService.Object);
        }

        [Theory]
        [InlineData(null, 3)]
        [InlineData("approved", 2)]
        [InlineData("rejected", 1)]
        [InlineData("random string", 3)]
        public async Task GetOrdersListTest(string status, int expectedCount)
        {
            var result = await _ordersController.GetOrderList(status);

            var jsonResult = Assert.IsType<JsonResult>(result);

            var orders = jsonResult.Value.GetType().GetProperty("data")?.GetValue(jsonResult.Value, null) as IEnumerable<OrderHeader>;

            orders.ToList().Count.ShouldBe(expectedCount);

            // GetOrders() should only be called once
            _orderService.Verify(x => x.GetOrders(), Times.Once);
        }

        [Fact]
        public async Task OrderDetailsPageTest()
        {
            var result = await _ordersController.Details(It.IsAny<int>());

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<OrderDetailsVM>(viewResult.ViewData.Model);

            // GetOrderDetails() should only be called once
            _orderService.Verify(x => x.GetOrderDetails(It.IsAny<int>()), Times.Once);
        }
    }
}
