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
    public static class MockMobileSuitCartRepository
    {
        public static Mock<IMobileSuitCartRepository> GetMobileSuitCartRepository()
        {
            var mobileSuitCartItems = new List<MobileSuitCartItem>
            {
                new MobileSuitCartItem
                {
                    Id = 1,
                    ApplicationUserId = "user1",
                    MobileSuitId = 1,
                    Price = 45000m
                },
                new MobileSuitCartItem
                {
                    Id = 2,
                    ApplicationUserId = "user1",
                    MobileSuitId = 2,
                    Price = 47000m
                },
                new MobileSuitCartItem
                {
                    Id = 3,
                    ApplicationUserId = "user2",
                    MobileSuitId = 4,
                    Price = 48000m
                },
            };

            var mockRepo = new Mock<IMobileSuitCartRepository>();

            mockRepo.Setup(r => r.GetAllCartItems(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var userMobileSuitCartItems = mobileSuitCartItems.Where(w => w.ApplicationUserId == userId).ToList();

                return userMobileSuitCartItems;
            });

            mockRepo.Setup(r => r.Add(It.IsAny<MobileSuitCartItem>())).ReturnsAsync((MobileSuitCartItem mobileSuitCartItem) =>
            {
                mobileSuitCartItem.Id = mobileSuitCartItems.Last().Id + 1;

                mobileSuitCartItems.Add(mobileSuitCartItem);

                return mobileSuitCartItem;
            });

            mockRepo.Setup(r => r.CartItemExists(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((string userId, int mobileSuitId) =>
            {
                var mobileSuitCartItem = mobileSuitCartItems.Where(m => m.ApplicationUserId == userId)
                                                            .Where(m => m.MobileSuitId == mobileSuitId)
                                                            .FirstOrDefault();

                if (mobileSuitCartItem is not null)
                    return true;
                else
                    return false;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int mobileSuitCartItemId) =>
            {
                var mobileSuitCartItem = mobileSuitCartItems.Where(m => m.Id == mobileSuitCartItemId)
                                                            .FirstOrDefault();

                return mobileSuitCartItem;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<MobileSuitCartItem>())).Returns((MobileSuitCartItem mobileSuitCartItem) =>
            {
                var userMobileSuitCartItem = mobileSuitCartItems.Where(m => m.Id == mobileSuitCartItem.Id)
                                                        .FirstOrDefault();

                mobileSuitCartItems.Remove(userMobileSuitCartItem);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
