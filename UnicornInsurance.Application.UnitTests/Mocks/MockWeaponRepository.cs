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
    public static class MockWeaponRepository
    {
        public static Mock<IWeaponRepository> GetWeaponRepository()
        {
            var weapons = new List<Weapon>
            {
                new Weapon
                {
                    Id = 1,
                    Name = "Beam Rifle",
                    Description = "A compact Mega-Particle Cannon.",
                    Price = 1750m,
                    IsCustomWeapon = false
                },
                new Weapon
                {
                    Id = 2,
                    Name = "Beam Spray Gun",
                    Description = "A short-range version of the Beam Rifle.",
                    Price = 1650m,
                    IsCustomWeapon = false
                },
                new Weapon
                {
                    Id = 3,
                    Name = "Beam Saber",
                    Description = "Emits high-energy Minovsky particles, which are then contained by a blade-shaped I-field",
                    Price = 1850m,
                    IsCustomWeapon = false
                },
                new Weapon
                {
                    Id = 6,
                    Name = "NewType-Destroyer System",
                    Description = "An operating system designed with the specific purpose of combating NewTypes.",
                    Price = 3000m,
                    IsCustomWeapon = true
                }
            };

            var mockRepo = new Mock<IWeaponRepository>();

            mockRepo.Setup(r => r.GetStandardWeaponsList())
                                 .ReturnsAsync(weapons.Where(w => w.IsCustomWeapon == false)
                                                      .ToList());            

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var weapon = weapons.Where(w => w.Id == id).FirstOrDefault();
                return weapon;
            });

            mockRepo.Setup(r => r.Add(It.IsAny<Weapon>())).ReturnsAsync((Weapon weapon) =>
            {
                weapons.Add(weapon);
                return weapon;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<Weapon>())).Returns((Weapon weapon) =>
            {
                var weaponToDelete = weapons.Where(w => w.Id == weapon.Id)
                                            .FirstOrDefault();

                weapons.Remove(weaponToDelete);

                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
