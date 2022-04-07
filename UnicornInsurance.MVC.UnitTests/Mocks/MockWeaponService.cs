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
    public static class MockWeaponService
    {
        public static Mock<IWeaponService> GetWeaponService()
        {
            var weapons = new List<Weapon>
            {
                new Weapon
                {
                    Id = 1,
                    Name = "Beam Rifle",
                    Description = "very powerful",
                    Price = 2000m,
                    ImageUrl = ""
                },
                new Weapon
                {
                    Id = 2,
                    Name = "Beam Spray Gun",
                    Description = "super powerful",
                    Price = 1500m,
                    ImageUrl = ""
                },
                new Weapon
                {
                    Id = 3,
                    Name = "Beam Saber",
                    Description = "melee weapon",
                    Price = 1000m,
                    ImageUrl = ""
                },
            };

            var mockRepo = new Mock<IWeaponService>();

            mockRepo.Setup(r => r.GetWeapons()).ReturnsAsync(weapons);

            mockRepo.Setup(r => r.GetWeaponDetails(It.IsAny<int>())).ReturnsAsync((int weaponId) =>
            {
                var weapon = weapons.Where(w => w.Id == weaponId).FirstOrDefault();
                return weapon;
            });

            mockRepo.Setup(r => r.UpdateWeapon(It.IsAny<Weapon>())).ReturnsAsync((Weapon weapon) =>
            {
                var weaponToUpdate = weapons.Where(w => w.Id == weapon.Id).FirstOrDefault();

                if (weaponToUpdate is null)
                    return new BaseCommandResponse() { Success = false, Message = "Failure" };
                else
                    return new BaseCommandResponse() { Success = true, Message = "Success" };
            });

            mockRepo.Setup(r => r.InsertWeapon(It.IsAny<Weapon>())).ReturnsAsync((Weapon weapon) =>
            {
                return new BaseCommandResponse() { Success = true, Message = "Success" };
            });

            return mockRepo;
        }
    }
}
