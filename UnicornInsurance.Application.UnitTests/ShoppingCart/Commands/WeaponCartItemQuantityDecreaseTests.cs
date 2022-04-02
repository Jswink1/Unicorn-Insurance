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
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Commands
{
    public class WeaponCartItemQuantityDecreaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public WeaponCartItemQuantityDecreaseTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }        

        [Test]
        [Order(1)]
        [TestCase(2)]
        public async Task Valid_WeaponCartItem_QuantityDecreased(int weaponId)
        {
            var handler = new WeaponCartItemQuantityDecreaseHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            var userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem("user1", weaponId);
            var count = userWeaponCartItem.Count;

            await handler.Handle(new WeaponCartItemQuantityDecreaseCommand() { WeaponId = weaponId }, CancellationToken.None);

            userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem("user1", weaponId);
            userWeaponCartItem.Count.ShouldBe(count - 1);
        }

        private static IEnumerable<object[]> InvalidItemQuantityDecreaseValues()
        {
            yield return new object[] { 7, _user1HttpContext, "user1" };
            yield return new object[] { -2342, _user1HttpContext, "user1" };
            yield return new object[] { -25, _user2HttpContext, "user2" };
            yield return new object[] { 4365, _user2HttpContext, "user2" };
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(InvalidItemQuantityDecreaseValues))]
        public async Task Invalid_WeaponCartItem_QuantityDecreased(int weaponId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new WeaponCartItemQuantityDecreaseHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new WeaponCartItemQuantityDecreaseCommand() { WeaponId = weaponId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();

            var userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem(userId, weaponId);
            userWeaponCartItem.ShouldBeNull();
        }

        private static IEnumerable<object[]> ItemQuantityDecreaseValues_ThatThrowDecreaseException()
        {
            yield return new object[] { 1, _user1HttpContext, "user1" };
            yield return new object[] { 2, _user2HttpContext, "user2" };
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(ItemQuantityDecreaseValues_ThatThrowDecreaseException))]
        public async Task WeaponCartItem_QuantityDecrease_ThrowsDecreaseException(int weaponId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new WeaponCartItemQuantityDecreaseHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new WeaponCartItemQuantityDecreaseCommand() { WeaponId = weaponId }, CancellationToken.None)
                         .ShouldThrowAsync<DecreaseItemQuantityException>();

            var userWeaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem(userId, weaponId);
            userWeaponCartItem.Count.ShouldBe(1);
        }
    }
}
