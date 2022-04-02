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
    public static class MockUserWeaponRepository
    {
        public static Mock<IUserWeaponRepository> GetUserWeaponRepository()
        {
            var userWeapons = new List<UserWeapon>
            {
                new UserWeapon
                {
                    Id = 1,
                    ApplicationUserId = "user1",
                    WeaponId = 6,
                    EquippedMobileSuitId = 1,
                    IsCustomWeapon = true
                },
                new UserWeapon
                {
                    Id = 2,
                    ApplicationUserId = "user1",
                    WeaponId = 1,
                    EquippedMobileSuitId = null,
                    IsCustomWeapon = false
                },
                new UserWeapon
                {
                    Id = 3,
                    ApplicationUserId = "user2",
                    WeaponId = 2,
                    EquippedMobileSuitId = null,
                    IsCustomWeapon = false
                },
                new UserWeapon
                {
                    Id = 4,
                    ApplicationUserId = "user2",
                    WeaponId = 3,
                    EquippedMobileSuitId = 4,
                    IsCustomWeapon = false
                }
            };

            var mockRepo = new Mock<IUserWeaponRepository>();

            mockRepo.Setup(r => r.CreateUserWeapon(It.IsAny<string>(), It.IsAny<WeaponPurchase>()))
                    .Returns((string userId, WeaponPurchase weapon) =>
            {
                UserWeapon userWeapon = new() { ApplicationUserId = userId, WeaponId = weapon.Id };

                userWeapon.Id = userWeapons.Last().Id + 1;
                userWeapons.Add(userWeapon);

                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.GetAvailableUserWeapons(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var userItems = userWeapons.Where(i => i.ApplicationUserId == userId).ToList();

                return userItems;
            });

            mockRepo.Setup(r => r.GetUserMobileSuitEquippedWeapon(It.IsAny<int>())).ReturnsAsync((int userMobileSuitId) =>
            {
                var equippedWeapons = userWeapons.Where(i => i.EquippedMobileSuitId == userMobileSuitId).ToList();
                var equippedWeapon = equippedWeapons.Where(i => i.IsCustomWeapon == false).FirstOrDefault();
                
                return equippedWeapon;
            });

            mockRepo.Setup(r => r.GetUserMobileSuitCustomWeapon(It.IsAny<int>())).ReturnsAsync((int userMobileSuitId) =>
            {
                var equippedWeapons = userWeapons.Where(i => i.EquippedMobileSuitId == userMobileSuitId).ToList();
                var customWeapon = equippedWeapons.Where(i => i.IsCustomWeapon == true).FirstOrDefault();

                return customWeapon;
            });

            mockRepo.Setup(r => r.GetAvailableUserWeapons(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var availableWeapons = userWeapons.Where(i => i.ApplicationUserId == userId)
                                                  .Where(i => i.EquippedMobileSuitId == null)
                                                  .ToList();

                return availableWeapons;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int userWeaponId) =>
            {
                var userItem = userWeapons.Where(i => i.Id == userWeaponId).FirstOrDefault();

                return userItem;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<UserWeapon>())).Returns((UserWeapon userWeapon) =>
            {
                var userItem = userWeapons.Where(i => i.Id == userWeapon.Id)
                                          .FirstOrDefault();

                userWeapons.Remove(userItem);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
