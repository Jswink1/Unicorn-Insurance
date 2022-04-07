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
    public static class MockShoppingCartService
    {
        public static Mock<IShoppingCartService> GetShopingCartService()
        {
            var mockRepo = new Mock<IShoppingCartService>();

            mockRepo.Setup(r => r.GetAllMobileSuitCartItems()).ReturnsAsync(new List<MobileSuitCartItem>());

            mockRepo.Setup(r => r.GetAllWeaponCartItems()).ReturnsAsync(new List<WeaponCartItem>());

            return mockRepo;
        }
    }
}
