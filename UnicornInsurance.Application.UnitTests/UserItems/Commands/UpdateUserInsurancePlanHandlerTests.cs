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
using UnicornInsurance.Application.Features.UserItems.Handlers.Commands;
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.UserItems.Commands
{
    public class UpdateUserInsurancePlanHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public UpdateUserInsurancePlanHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ValidUpdateUserInsuranceValues()
        {
            yield return new object[] 
            { 
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.StandardInsurancePlan,
                    UserMobileSuitId = 1
                }
            };
            yield return new object[]
            {
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.SuperInsurancePlan,
                    UserMobileSuitId = 2
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.UltraInsurancePlan,
                    UserMobileSuitId = 4
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(ValidUpdateUserInsuranceValues))]
        public async Task Valid_UpdateUserInsurancePlan(Mock<IHttpContextAccessor> userHttpContext, UserInsurancePlanDTO userInsurancePlanDTO)
        {
            var handler = new UpdateUserInsurancePlanHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var response = await handler.Handle(new UpdateUserInsurancePlanCommand() { UserInsurancePlanDTO = userInsurancePlanDTO }, CancellationToken.None);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeTrue();

            var userMobileSuit = await _mockUnitOfWork.Object.UserMobileSuitRepository.Get(userInsurancePlanDTO.UserMobileSuitId);
            userMobileSuit.InsurancePlan.ShouldBe(userInsurancePlanDTO.InsurancePlan);
            userMobileSuit.EndOfCoverage.ShouldBe(DateTime.UtcNow.AddDays(1), TimeSpan.FromMinutes(1));
        }

        private static IEnumerable<object[]> InvalidUpdateUserInsuranceValues()
        {
            yield return new object[]
            {
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = "Random Insurance Plan",
                    UserMobileSuitId = 1
                }
            };
            yield return new object[]
            {
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    UserMobileSuitId = 2
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.UltraInsurancePlan,
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(InvalidUpdateUserInsuranceValues))]
        public async Task Invalid_UpdateUserInsurancePlan(Mock<IHttpContextAccessor> userHttpContext, UserInsurancePlanDTO userInsurancePlanDTO)
        {
            var handler = new UpdateUserInsurancePlanHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var response = await handler.Handle(new UpdateUserInsurancePlanCommand() { UserInsurancePlanDTO = userInsurancePlanDTO }, CancellationToken.None);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
        }

        private static IEnumerable<object[]> NotFoundUpdateUserInsuranceValues()
        {
            yield return new object[]
            {
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.StandardInsurancePlan,
                    UserMobileSuitId = -1
                }
            };
            yield return new object[]
            {
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.SuperInsurancePlan,
                    UserMobileSuitId = 345
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(NotFoundUpdateUserInsuranceValues))]
        public async Task UpdateUserInsurancePlan_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, UserInsurancePlanDTO userInsurancePlanDTO)
        {
            var handler = new UpdateUserInsurancePlanHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new UpdateUserInsurancePlanCommand() { UserInsurancePlanDTO = userInsurancePlanDTO }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> UnauthorizedUpdateUserInsuranceValues()
        {
            yield return new object[]
            {
                _user1HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.StandardInsurancePlan,
                    UserMobileSuitId = 4
                }
            };
            yield return new object[]
            {
                _user2HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.SuperInsurancePlan,
                    UserMobileSuitId = 1
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(UnauthorizedUpdateUserInsuranceValues))]
        public async Task UpdateUserInsurancePlan_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, UserInsurancePlanDTO userInsurancePlanDTO)
        {
            var handler = new UpdateUserInsurancePlanHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new UpdateUserInsurancePlanCommand() { UserInsurancePlanDTO = userInsurancePlanDTO }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }

        private static IEnumerable<object[]> MobileSuitDamagedUpdateUserInsuranceValues()
        {
            yield return new object[]
            {
                _user2HttpContext,
                new UserInsurancePlanDTO
                {
                    InsurancePlan = SD.SuperInsurancePlan,
                    UserMobileSuitId = 6
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(MobileSuitDamagedUpdateUserInsuranceValues))]
        public async Task UpdateUserInsurancePlan_ShouldThrow_MobileSuitDamagedException(Mock<IHttpContextAccessor> userHttpContext, UserInsurancePlanDTO userInsurancePlanDTO)
        {
            var handler = new UpdateUserInsurancePlanHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new UpdateUserInsurancePlanCommand() { UserInsurancePlanDTO = userInsurancePlanDTO }, CancellationToken.None)
                         .ShouldThrowAsync<MobileSuitDamagedException>();
        }
    }
}
