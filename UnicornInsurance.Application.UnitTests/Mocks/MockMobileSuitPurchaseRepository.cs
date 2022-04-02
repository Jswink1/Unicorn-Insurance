using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.UnitTests.Mocks
{
    public static class MockMobileSuitPurchaseRepository
    {
        public static Mock<IMobileSuitPurchaseRepository> GetMobileSuitPurchaseRepository()
        {
            var mobileSuitPurchases = new List<MobileSuitPurchase>
            {
                new MobileSuitPurchase()
                {
                    Id = 1,                    
                    OrderId = 1,
                    MobileSuitId = 1,
                    Price = 10000m                    
                },
                new MobileSuitPurchase()
                {
                    Id = 2,
                    OrderId = 2,
                    MobileSuitId = 2,
                    Price = 10000m
                }
            };

            var mockRepo = new Mock<IMobileSuitPurchaseRepository>();

            mockRepo.Setup(r => r.Add(It.IsAny<MobileSuitPurchase>())).ReturnsAsync((MobileSuitPurchase mobileSuitPurchase) =>
            {
                mobileSuitPurchase.Id = mobileSuitPurchases.Last().Id + 1;
                mobileSuitPurchases.Add(mobileSuitPurchase);

                return mobileSuitPurchase;
            });

            mockRepo.Setup(r => r.GetMobileSuitPurchasesForOrder(It.IsAny<int>())).ReturnsAsync((int orderId) =>
            {
                List<MobileSuitPurchase> userPurchases = mobileSuitPurchases.Where(p => p.OrderId == orderId).ToList();

                return userPurchases;
            });

            return mockRepo;
        }
    }
}
