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
    public static class MockWeaponPurchaseRepository
    {
        public static Mock<IWeaponPurchaseRepository> GetWeaponPurchaseRepository()
        {
            var weaponPurchases = new List<WeaponPurchase>
            {
                new WeaponPurchase
                {
                    Id = 1,
                    OrderId = 1,
                    WeaponId = 1,
                    Count = 1,
                    Price = 5000m
                },
                new WeaponPurchase
                {
                    Id = 2,
                    OrderId = 2,
                    WeaponId = 2,
                    Count = 2,
                    Price = 5000m
                }
            };

            var mockRepo = new Mock<IWeaponPurchaseRepository>();

            mockRepo.Setup(r => r.Add(It.IsAny<WeaponPurchase>())).ReturnsAsync((WeaponPurchase weaponPurchase) =>
            {
                weaponPurchase.Id = weaponPurchases.Last().Id + 1;
                weaponPurchases.Add(weaponPurchase);

                return weaponPurchase;
            });

            mockRepo.Setup(r => r.GetWeaponPurchasesForOrder(It.IsAny<int>())).ReturnsAsync((int orderId) =>
            {
                List<WeaponPurchase> userPurchases = weaponPurchases.Where(p => p.OrderId == orderId).ToList();

                return userPurchases;
            });

            return mockRepo;
        }
    }
}
