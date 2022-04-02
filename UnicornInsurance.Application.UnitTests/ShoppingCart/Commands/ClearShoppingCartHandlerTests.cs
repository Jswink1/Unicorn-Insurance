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
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Commands
{
    public class ClearShoppingCartHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public ClearShoppingCartHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> UserHttpContextsValues()
        {
            yield return new object[] { _user1HttpContext, "user1" };
            yield return new object[] { _user2HttpContext, "user2" };
        }

        [Test]
        [TestCaseSource(nameof(UserHttpContextsValues))]
        public async Task ClearShoppingCartTest(Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new ClearShoppingCartHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new ClearShoppingCartCommand(), CancellationToken.None);

            var weaponCartItems = await _mockUnitOfWork.Object.WeaponCartRepository.GetAllCartItems(userId);
            weaponCartItems.Count.ShouldBe(0);

            var mobileSuitCartItems = await _mockUnitOfWork.Object.MobileSuitCartRepository.GetAllCartItems(userId);
            mobileSuitCartItems.Count.ShouldBe(0);
        }
    }
}
