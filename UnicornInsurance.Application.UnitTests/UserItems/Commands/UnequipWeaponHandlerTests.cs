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
using UnicornInsurance.Application.Features.UserItems.Handlers.Commands;
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.UserItems.Commands
{
    public class UnequipWeaponHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public UnequipWeaponHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> UnequipWeaponValues()
        {
            yield return new object[] { _user2HttpContext, 4 };
        }

        [Test]
        [TestCaseSource(nameof(UnequipWeaponValues))]
        public async Task UnequipWeaponTest(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new UnequipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new UnequipWeaponCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None);

            var equippedWeapon = await _mockUnitOfWork.Object.UserWeaponRepository.GetUserMobileSuitEquippedWeapon(userMobileSuitId);
            equippedWeapon.ShouldBeNull();
        }

        private static IEnumerable<object[]> UnequipWeaponNotFoundValues()
        {
            yield return new object[] { _user1HttpContext, 15 };
            yield return new object[] { _user1HttpContext, -2};
            yield return new object[] { _user2HttpContext, 2344 };
        }

        [Test]
        [TestCaseSource(nameof(UnequipWeaponNotFoundValues))]
        public async Task UnequipWeapon_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new UnequipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new UnequipWeaponCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> UnequipWeaponUnauthorizedValues()
        {
            yield return new object[] { _user1HttpContext, 4 };
            yield return new object[] { _user2HttpContext, 1 };
            yield return new object[] { _user2HttpContext, 2 };
        }

        [Test]
        [TestCaseSource(nameof(UnequipWeaponUnauthorizedValues))]
        public async Task UnequipWeapon_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new UnequipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new UnequipWeaponCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                .ShouldThrowAsync<UnauthorizedAccessException>();
        }
    }
}
