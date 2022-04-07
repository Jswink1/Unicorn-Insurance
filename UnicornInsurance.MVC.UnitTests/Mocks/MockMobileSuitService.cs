using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.UnitTests.Mocks
{
    public static class MockMobileSuitService
    {
        public static Mock<IMobileSuitService> GetMobileSuitService()
        {
            var mobileSuits = new List<MobileSuit>
            {
                new MobileSuit
                {
                    Id = 1,
                    Name = "Unicorn Gundam",
                    Description = "The coolest mobile suit",
                    Price = 20000m,
                    ImageUrl = ""
                },
                new MobileSuit
                {
                    Id = 2,
                    Name = "00 Gundam",
                    Description = "The fastest mobile suit",
                    Price = 15000m,
                    ImageUrl = ""
                },
                new MobileSuit
                {
                    Id = 3,
                    Name = "RX-78 Gundam",
                    Description = "The original mobile suit",
                    Price = 10000m,
                    ImageUrl = ""
                }
            };

            var mockRepo = new Mock<IMobileSuitService>();

            mockRepo.Setup(r => r.GetMobileSuits()).ReturnsAsync(mobileSuits);

            mockRepo.Setup(r => r.GetMobileSuitDetails(It.IsAny<int>())).ReturnsAsync((int mobileSuitId) =>
            {
                var mobileSuit = mobileSuits.Where(m => m.Id == mobileSuitId).FirstOrDefault();
                return mobileSuit;
            });

            mockRepo.Setup(r => r.UpdateMobileSuit(It.IsAny<MobileSuit>())).ReturnsAsync((MobileSuit mobileSuit) =>
            {
                var mobileSuitToUpdate = mobileSuits.Where(m => m.Id == mobileSuit.Id).FirstOrDefault();

                if (mobileSuitToUpdate is null)
                    return new BaseCommandResponse() { Success = false, Message = "Failure" };
                else
                    return new BaseCommandResponse() { Success = true, Message = "Success" };
            });

            mockRepo.Setup(r => r.InsertMobileSuit(It.IsAny<MobileSuit>())).ReturnsAsync((MobileSuit mobileSuit) =>
            {
                return new BaseCommandResponse() { Success = true, Message = "Success" };
            });

            return mockRepo;
        }
    }
}
