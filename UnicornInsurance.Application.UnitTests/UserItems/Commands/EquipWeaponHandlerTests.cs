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
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.UserItems.Handlers.Commands;
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.UserItems.Commands
{
    public class EquipWeaponHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public EquipWeaponHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ValidEquipWeaponValues()
        {
            yield return new object[]
            { 
                _user1HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 2,
                    SelectedWeaponId = 2
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 5,
                    SelectedWeaponId = 3
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 6,
                    SelectedWeaponId = 3
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(ValidEquipWeaponValues))]
        public async Task Valid_EquipWeapon(Mock<IHttpContextAccessor> userHttpContext, EquipWeaponDTO equipWeaponDTO)
        {
            var handler = new EquipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var result = await handler.Handle(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
        }

        private static IEnumerable<object[]> InvalidEquipWeaponValues()
        {
            yield return new object[]
            {
                _user1HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 2
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    SelectedWeaponId = 3
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO { }
            };
        }

        [Test]
        [TestCaseSource(nameof(InvalidEquipWeaponValues))]
        public async Task Invalid_EquipWeapon(Mock<IHttpContextAccessor> userHttpContext, EquipWeaponDTO equipWeaponDTO)
        {
            var handler = new EquipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var result = await handler.Handle(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }

        private static IEnumerable<object[]> EquipWeaponNotFoundValues()
        {
            yield return new object[]
            {
                _user1HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 2,
                    SelectedWeaponId = 345
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = -5,
                    SelectedWeaponId = 3
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 9000,
                    SelectedWeaponId = 17
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(EquipWeaponNotFoundValues))]
        public async Task EquipWeapon_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, EquipWeaponDTO equipWeaponDTO)
        {
            var handler = new EquipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> EquipWeaponUnauthorizedValues()
        {
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 2,
                    SelectedWeaponId = 2
                }
            };
            yield return new object[]
            {
                _user1HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 3,
                    SelectedWeaponId = 3
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(EquipWeaponUnauthorizedValues))]
        public async Task EquipWeapon_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, EquipWeaponDTO equipWeaponDTO)
        {
            var handler = new EquipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }

        private static IEnumerable<object[]> EquipCustomWeaponExceptionValues()
        {
            yield return new object[]
            {
                _user1HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 2,
                    SelectedWeaponId = 1
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(EquipCustomWeaponExceptionValues))]
        public async Task EquipWeapon_ShouldThrow_EquipCustomWeaponException(Mock<IHttpContextAccessor> userHttpContext, EquipWeaponDTO equipWeaponDTO)
        {
            var handler = new EquipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO }, CancellationToken.None)
                         .ShouldThrowAsync<EquipCustomWeaponException>();
        }

        private static IEnumerable<object[]> WeaponEquippedExceptionValues()
        {
            yield return new object[]
            {
                _user2HttpContext,
                new EquipWeaponDTO
                {
                    UserMobileSuitId = 4,
                    SelectedWeaponId = 4
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(WeaponEquippedExceptionValues))]
        public async Task EquipWeapon_ShouldThrow_WeaponEquippedException(Mock<IHttpContextAccessor> userHttpContext, EquipWeaponDTO equipWeaponDTO)
        {
            var handler = new EquipWeaponHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO }, CancellationToken.None)
                         .ShouldThrowAsync<WeaponEquippedException>();
        }
    }
}
