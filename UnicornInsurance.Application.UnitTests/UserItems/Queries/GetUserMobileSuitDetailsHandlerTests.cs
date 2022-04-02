using AutoMapper;
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
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.UserItems.Handlers.Queries;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.UserItems.Queries
{
    public class GetUserMobileSuitDetailsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user3HttpContext = new();

        public GetUserMobileSuitDetailsHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
            _user3HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user3"));
        }

        private static IEnumerable<object[]> GetUserMobileSuitDetailsValues()
        {
            yield return new object[] { _user1HttpContext, 1, 1, null, true, };
            yield return new object[] { _user1HttpContext, 2, 1, null, null };
            yield return new object[] { _user2HttpContext, 4, 1, null, null };
            yield return new object[] { _user2HttpContext, 4, 1, null, null };
        }

        [Test]
        [TestCaseSource(nameof(GetUserMobileSuitDetailsValues))]
        public async Task GetUserMobileSuitDetailsTest(Mock<IHttpContextAccessor> userHttpContext, 
                                                       int userMobileSuitId,
                                                       int availableWeaponsCount,
                                                       bool equippedWeaponStatus,
                                                       bool customWeaponStatus)
        {
            var handler = new GetUserMobileSuitDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            var result = await handler.Handle(new GetUserMobileSuitDetailsRequest() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None);

            result.ShouldBeOfType<FullUserMobileSuitDTO>();
            result.AvailableWeapons.Count.ShouldBe(availableWeaponsCount);

            if (result.EquippedWeapon is not null)
            {
                result.EquippedWeapon.IsCustomWeapon.ShouldBe(equippedWeaponStatus);
            }
            if (result.CustomWeapon is not null)
            {
                result.CustomWeapon.IsCustomWeapon.ShouldBe(customWeaponStatus);
            }
        }

        private static IEnumerable<object[]> GetNotFoundUserMobileSuitDetailsValues()
        {
            yield return new object[] { _user1HttpContext, 34 };
            yield return new object[] { _user1HttpContext, 0 };
            yield return new object[] { _user2HttpContext, -2345 };
        }

        [Test]
        [TestCaseSource(nameof(GetNotFoundUserMobileSuitDetailsValues))]
        public async Task GetUserMobileSuitDetails_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new GetUserMobileSuitDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new GetUserMobileSuitDetailsRequest() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> GetUnauthorizedUserMobileSuitDetailsValues()
        {
            yield return new object[] { _user1HttpContext, 4 };
            yield return new object[] { _user2HttpContext, 1 };
        }

        [Test]
        [TestCaseSource(nameof(GetUnauthorizedUserMobileSuitDetailsValues))]
        public async Task GetUserMobileSuitDetails_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new GetUserMobileSuitDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new GetUserMobileSuitDetailsRequest() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }
    }
}
