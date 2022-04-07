using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.UnitTests.Mocks
{
    public class MockUserItemService
    {
        public static Mock<IUserItemService> GetUserItemService()
        {
            var userMobileSuits = new List<UserMobileSuit>()
            {
                new UserMobileSuit
                {
                    Id = 1,
                    AvailableWeapons = new List<UserWeapon>()
                    {
                        new UserWeapon
                        {
                            Id = 1,
                            Weapon = new Weapon() { Id = 1, Name = "Beam Rifle"}
                        },
                        new UserWeapon
                        {
                            Id = 2,
                            Weapon = new Weapon() { Id = 2, Name = "Beam Spray Gun"}
                        }
                    }
                },
                new UserMobileSuit
                {
                    Id = 2,
                    AvailableWeapons = new List<UserWeapon>()
                    {
                        new UserWeapon
                        {
                            Id = 3,
                            Weapon = new Weapon() { Id = 3, Name = "Beam Saber"}
                        }
                    }
                }
            };

            var mockRepo = new Mock<IUserItemService>();

            mockRepo.Setup(r => r.GetAllUserMobileSuits()).ReturnsAsync(new List<UserMobileSuit>());

            mockRepo.Setup(r => r.GetUserMobileSuitDetails(It.IsAny<int>())).ReturnsAsync((int userMobileSuitId) =>
            {
                var userMobileSuit = userMobileSuits.Where(i => i.Id == userMobileSuitId).FirstOrDefault();

                return userMobileSuit;
            });

            return mockRepo;
        }
    }
}
