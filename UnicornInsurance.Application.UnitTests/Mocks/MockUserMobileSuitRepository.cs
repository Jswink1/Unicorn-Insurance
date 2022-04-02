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
    public static class MockUserMobileSuitRepository
    {
        public static Mock<IUserMobileSuitRepository> GetUserMobileSuitRepository()
        {
            var userMobileSuits = new List<UserMobileSuit>
            {
                new UserMobileSuit
                {
                    Id = 1,
                    ApplicationUserId = "user1",
                    MobileSuitId = 1,
                    InsurancePlan = SD.SuperInsurancePlan,
                    EndOfCoverage = DateTime.UtcNow.AddDays(1)
                },
                new UserMobileSuit
                {
                    Id = 2,
                    ApplicationUserId = "user1",
                    MobileSuitId = 4,
                    InsurancePlan = SD.StandardInsurancePlan,
                    EndOfCoverage = DateTime.UtcNow.AddDays(1)
                },
                new UserMobileSuit
                {
                    Id = 3,
                    ApplicationUserId = "user1",
                    MobileSuitId = 5,
                    InsurancePlan = SD.UltraInsurancePlan,
                    EndOfCoverage = DateTime.UtcNow.AddDays(1)
                },
                new UserMobileSuit
                {
                    Id = 4,
                    ApplicationUserId = "user2",
                    MobileSuitId = 1,
                    InsurancePlan = SD.StandardInsurancePlan,
                    EndOfCoverage = DateTime.UtcNow.AddDays(1)
                },
                new UserMobileSuit
                {
                    Id = 5,
                    ApplicationUserId = "user2",
                    MobileSuitId = 4,
                    InsurancePlan = SD.StandardInsurancePlan,
                    EndOfCoverage = DateTime.UtcNow.AddDays(-1)
                },
                new UserMobileSuit
                {
                    Id = 6,
                    ApplicationUserId = "user2",
                    MobileSuitId = 5,
                    IsDamaged = true                    
                }
            };

            var mockRepo = new Mock<IUserMobileSuitRepository>();

            mockRepo.Setup(r => r.CreateUserMobileSuit(It.IsAny<string>(), It.IsAny<MobileSuitPurchase>()))
                    .Returns((string userId, MobileSuitPurchase mobileSuit) =>
            {
                UserMobileSuit userMobileSuit = new() { ApplicationUserId = userId, MobileSuitId = mobileSuit.Id };

                userMobileSuit.Id = userMobileSuits.Last().Id + 1;
                userMobileSuits.Add(userMobileSuit);

                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.GetAllUserMobileSuits(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var userItems = userMobileSuits.Where(i => i.ApplicationUserId == userId).ToList();

                return userItems;
            });

            mockRepo.Setup(r => r.GetUserMobileSuit(It.IsAny<int>())).ReturnsAsync((int userMobileSuitId) =>
            {
                var userItem = userMobileSuits.Where(i => i.Id == userMobileSuitId).FirstOrDefault();

                return userItem;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int userMobileSuitId) =>
            {
                var userItem = userMobileSuits.Where(i => i.Id == userMobileSuitId).FirstOrDefault();

                return userItem;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<UserMobileSuit>())).Returns((UserMobileSuit userMobileSuit) =>
            {
                var userItem = userMobileSuits.Where(i => i.Id == userMobileSuit.Id)
                                              .FirstOrDefault();

                userMobileSuits.Remove(userItem);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
