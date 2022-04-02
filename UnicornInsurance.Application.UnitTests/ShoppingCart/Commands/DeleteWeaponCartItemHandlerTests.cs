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
    public class DeleteWeaponCartItemHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteWeaponCartItemHandler _handler;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public DeleteWeaponCartItemHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new DeleteWeaponCartItemHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ValidWeaponCartItems()
        {
            yield return new object[] { 1, _user1HttpContext, "user1", 1 };
            yield return new object[] { 2, _user1HttpContext, "user1", 2 };
            yield return new object[] { 3, _user2HttpContext, "user2", 2 };
        }

        [Test]
        [TestCaseSource(nameof(ValidWeaponCartItems))]
        public async Task Valid_WeaponCartItem_Delete(int weaponCartItemId, Mock<IHttpContextAccessor> userHttpContext, string userId, int weaponId)
        {
            var handler = new DeleteWeaponCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteWeaponCartItemCommand() { WeaponCartItemId = weaponCartItemId }, CancellationToken.None);

            var weaponCartItem = await _mockUnitOfWork.Object.WeaponCartRepository.GetCartItem(userId, weaponId);
            weaponCartItem.ShouldBeNull();
        }

        [Test]
        [TestCase(23)]
        [TestCase(23246)]
        [TestCase(-2)]
        public async Task DeleteWeaponCartItem_ShouldThrow_NotFoundException(int weaponCartItemId)
        {
            await _handler.Handle(new DeleteWeaponCartItemCommand() { WeaponCartItemId = weaponCartItemId }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> UnauthorizedWeaponCartItems()
        {
            yield return new object[] { 3, _user1HttpContext, "user1", 2 };
            yield return new object[] { 1, _user2HttpContext, "user2", 1 };
            yield return new object[] { 2, _user2HttpContext, "user2", 2 };
        }

        [Test]
        [TestCaseSource(nameof(UnauthorizedWeaponCartItems))]
        public async Task DeleteWeaponCartItem_ShouldThrow_UnauthorizedAccessException(int weaponCartItemId, Mock<IHttpContextAccessor> userHttpContext, string userId, int weaponId)
        {
            var handler = new DeleteWeaponCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteWeaponCartItemCommand() { WeaponCartItemId = weaponCartItemId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }
    }
}
