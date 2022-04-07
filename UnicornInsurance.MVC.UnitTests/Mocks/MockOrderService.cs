using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.UnitTests.Mocks
{
    public class MockOrderService
    {
        public static Mock<IOrderService> GetOrderService()
        {
            var orders = new List<OrderHeader>()
            {
                new OrderHeader
                {
                    Id = 1,
                    PaymentStatus = SD.PaymentStatusApproved
                },
                new OrderHeader
                {
                    Id = 2,
                    PaymentStatus = SD.PaymentStatusApproved
                },
                new OrderHeader
                {
                    Id = 3,
                    PaymentStatus = SD.PaymentStatusRejected
                }
            };

            var mockRepo = new Mock<IOrderService>();

            mockRepo.Setup(r => r.GetOrders()).ReturnsAsync(orders);

            mockRepo.Setup(r => r.GetOrderDetails(It.IsAny<int>())).ReturnsAsync(new OrderDetailsVM());

            return mockRepo;
        }
    }
}
