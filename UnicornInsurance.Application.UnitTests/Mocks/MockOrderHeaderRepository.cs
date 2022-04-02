using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.UnitTests.Mocks
{
    public static class MockOrderHeaderRepository
    {
        public static Mock<IOrderHeaderRepository> GetOrderHeaderRepository()
        {
            var orderHeaders = new List<OrderHeader>
            {
                new OrderHeader()
                {
                    Id = 1,
                    ApplicationUserId = "user1",
                    OrderTotal = 15000m,
                    OrderDate = new DateTime(2022, 3, 28),
                    PaymentStatus = SD.PaymentStatusPending,
                    PaymentDate = new DateTime(2022, 3, 28)
                },
                new OrderHeader()
                {
                    Id = 2,
                    ApplicationUserId = "user2",
                    OrderTotal = 15000m,
                    OrderDate = new DateTime(2022, 3, 28),
                    PaymentStatus = SD.PaymentStatusApproved,
                    PaymentDate = new DateTime(2022, 3, 28)
                }
            };

            var mockRepo = new Mock<IOrderHeaderRepository>();

            mockRepo.Setup(r => r.Add(It.IsAny<OrderHeader>())).ReturnsAsync((OrderHeader orderHeader) =>
            {
                orderHeader.Id = orderHeaders.Last().Id + 1;
                orderHeaders.Add(orderHeader);

                return orderHeader;
            });

            mockRepo.Setup(r => r.GetUserOrders(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var userOrderHeaders = orderHeaders.Where(o => o.ApplicationUserId == userId).ToList();

                return userOrderHeaders;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var orderHeader = orderHeaders.Where(o => o.Id == id).FirstOrDefault();

                return orderHeader;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<OrderHeader>())).Returns((OrderHeader orderHeader) =>
            {
                var orderHeaderToDelete = orderHeaders.Where(o => o.Id == orderHeader.Id)
                                                      .FirstOrDefault();

                orderHeaders.Remove(orderHeaderToDelete);

                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(() =>
            {
                return orderHeaders;
            });

            return mockRepo;
        }
    }
}
