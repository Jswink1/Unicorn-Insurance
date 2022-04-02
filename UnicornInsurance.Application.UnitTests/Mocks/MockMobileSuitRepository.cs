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
    public static class MockMobileSuitRepository
    {
        public static Mock<IMobileSuitRepository> GetMobileSuitRepository()
        {
            var mobileSuits = new List<MobileSuit>
            {
                new MobileSuit
                {
                    Id = 1,
                    Name = "RX-0 Unicorn Gundam",
                    Description = "\"To my only desire, the symbol of hope, the beast of possibility...\"",
                    Price = 50000m
                },
                new MobileSuit
                {
                    Id = 2,
                    Name = "GN-001 Gundam Exia",
                    Description = "Also known as the \"Gundam Seven Swords\". Specializes in close-quarters combat.",
                    Price = 44999m
                },
                new MobileSuit
                {
                    Id = 4,
                    Name = "ASW-G-08 Gundam Barbatos Lupus Rex",
                    Description = "Customized for close combat, with an ferocious appearance.",
                    Price = 41000m
                },
                new MobileSuit
                {
                    Id = 5,
                    Name = "Sengoku Astray Gundam",
                    Description = "Aesthetically resembles a samurai, with two swords and  an enlarged V-fin that is modeled after the Crest-horn.",
                    Price = 42000m
                }
            };

            var mockRepo = new Mock<IMobileSuitRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(mobileSuits);

            mockRepo.Setup(r => r.GetFullMobileSuitDetails(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var mobileSuit = mobileSuits.Where(m => m.Id == id).FirstOrDefault();
                return mobileSuit;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var mobileSuit = mobileSuits.Where(m => m.Id == id).FirstOrDefault();
                return mobileSuit;
            });

            mockRepo.Setup(r => r.Add(It.IsAny<MobileSuit>())).ReturnsAsync((MobileSuit mobileSuit) =>
            {
                mobileSuit.Id = mobileSuits.Last().Id + 1;
                mobileSuits.Add(mobileSuit);
                return mobileSuit;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<MobileSuit>())).Returns((MobileSuit mobileSuit) =>
            {
                var mobileSuitToDelete = mobileSuits.Where(m => m.Id == mobileSuit.Id)
                                                    .FirstOrDefault();

                mobileSuits.Remove(mobileSuitToDelete);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
