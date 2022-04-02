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
    public class DeleteUserMobileSuitHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public DeleteUserMobileSuitHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> DeleteUserMobileSuitValues()
        {
            yield return new object[] { _user1HttpContext, 2 };
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(DeleteUserMobileSuitValues))]
        public async Task DeleteUserMobileSuitTest(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeleteUserMobileSuitHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteUserMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None);

            var userMobileSuit = await _mockUnitOfWork.Object.UserMobileSuitRepository.GetUserMobileSuit(userMobileSuitId);
            userMobileSuit.ShouldBeNull();
        }

        private static IEnumerable<object[]> DeleteUserMobileSuitNotFoundValues()
        {
            yield return new object[] { _user1HttpContext, 234 };
            yield return new object[] { _user1HttpContext, 15 };
            yield return new object[] { _user2HttpContext, -300 };
        }

        [Test]
        [TestCaseSource(nameof(DeleteUserMobileSuitNotFoundValues))]
        public async Task DeleteUserMobileSuit_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeleteUserMobileSuitHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteUserMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> DeleteUserMobileSuitUnauthorizedValues()
        {
            yield return new object[] { _user1HttpContext, 4 };
            yield return new object[] { _user2HttpContext, 1 };
            yield return new object[] { _user2HttpContext, 2 };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(DeleteUserMobileSuitUnauthorizedValues))]
        public async Task DeleteUserMobileSuit_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeleteUserMobileSuitHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteUserMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }

        private static IEnumerable<object[]> DeleteUserMobileSuitWithEquippedWeaponValues()
        {
            yield return new object[] { _user2HttpContext, 4 };
        }

        [Test]
        [TestCaseSource(nameof(DeleteUserMobileSuitWithEquippedWeaponValues))]
        public async Task DeleteUserMobileSuit_ShouldUnequip_EquippedWeapon(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeleteUserMobileSuitHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteUserMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None);

            var previouslyEquippedWeapon = await _mockUnitOfWork.Object.UserWeaponRepository.Get(4);
            previouslyEquippedWeapon.EquippedMobileSuitId.ShouldBeNull();
        }

        private static IEnumerable<object[]> DeleteUserMobileSuitWithCustomWeaponValues()
        {
            yield return new object[] { _user1HttpContext, 1 };
        }

        [Test]
        [TestCaseSource(nameof(DeleteUserMobileSuitWithCustomWeaponValues))]
        public async Task DeleteUserMobileSuit_ShouldDelete_CustomWeapon(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeleteUserMobileSuitHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteUserMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None);

            var previouslyEquippedWeapon = await _mockUnitOfWork.Object.UserWeaponRepository.Get(1);
            previouslyEquippedWeapon.ShouldBeNull();
        }
    }
}
