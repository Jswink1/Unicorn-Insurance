using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Commands
{
    public class WeaponCartItemQuantityIncreaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public WeaponCartItemQuantityIncreaseTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ValidItemQuantityIncreaseValues()
        {
            yield return new object[] { 1, _user1HttpContext, "user1" };
            yield return new object[] { 2, _user1HttpContext, "user1" };
            yield return new object[] { 2, _user2HttpContext, "user2" };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(ValidItemQuantityIncreaseValues))]
        public async Task Valid_WeaponCartItem_QuantityIncreased(int weaponId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new WeaponCartItemQuantityIncreaseHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem(userId, weaponId);
            var count = userWeaponCartItem.Count;

            await handler.Handle(new WeaponCartItemQuantityIncreaseCommand() { WeaponId = weaponId }, CancellationToken.None);

            userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem(userId, weaponId);
            userWeaponCartItem.Count.ShouldBe(count + 1);
        }

        [Test]
        [Order(2)]
        [TestCase(3)]
        [TestCase(3245)]
        [TestCase(-20)]
        public async Task Invalid_WeaponCartItem_QuantityIncreased(int weaponId)
        {
            var handler = new WeaponCartItemQuantityIncreaseHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            await handler.Handle(new WeaponCartItemQuantityIncreaseCommand() { WeaponId = weaponId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();

            var userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem("user1", weaponId);
            userWeaponCartItem.ShouldBeNull();
        }
    }
}
