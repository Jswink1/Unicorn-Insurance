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
    public static class MockWeaponCartRepository
    {
        public static Mock<IWeaponCartRepository> GetWeaponCartRepository()
        {
            var weaponCartItems = new List<WeaponCartItem>
            {
                new WeaponCartItem
                {
                    Id = 1,
                    ApplicationUserId = "user1",
                    WeaponId = 1,
                    Price = 1750m,
                    Count = 1
                },
                new WeaponCartItem
                {
                    Id = 2,
                    ApplicationUserId = "user1",
                    WeaponId = 2,
                    Price = 1850m,
                    Count = 2
                },
                new WeaponCartItem
                {
                    Id = 3,
                    ApplicationUserId = "user2",
                    WeaponId = 2,
                    Price = 2000m,
                    Count = 1
                },
            };

            var mockRepo = new Mock<IWeaponCartRepository>();

            mockRepo.Setup(r => r.GetAllCartItems(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var userWeaponCartItems = weaponCartItems.Where(w => w.ApplicationUserId == userId).ToList();

                return userWeaponCartItems;
            });

            mockRepo.Setup(r => r.Add(It.IsAny<WeaponCartItem>())).ReturnsAsync((WeaponCartItem weaponCartItem) =>
            {
                weaponCartItem.Id = weaponCartItems.Last().Id + 1;

                weaponCartItems.Add(weaponCartItem);

                return weaponCartItem;
            });

            mockRepo.Setup(r => r.GetCartItem(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((string userId, int weaponId) =>
            {
                var weaponCartItem = weaponCartItems.Where(w => w.ApplicationUserId == userId)
                                                    .Where(w => w.WeaponId == weaponId)
                                                    .FirstOrDefault();

                if (weaponCartItem is not null)
                {
                    weaponCartItem.Weapon = new Weapon()
                    {
                        Price = weaponCartItem.Price
                    };
                }

                return weaponCartItem;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int weaponCartItemId) =>
            {
                var weaponCartItem = weaponCartItems.Where(w => w.Id == weaponCartItemId)
                                                    .FirstOrDefault();

                return weaponCartItem;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<WeaponCartItem>())).Returns((WeaponCartItem weaponCartItem) =>
            {
                var userWeaponCartItem = weaponCartItems.Where(w => w.Id == weaponCartItem.Id)
                                                        .FirstOrDefault();

                weaponCartItems.Remove(userWeaponCartItem);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
