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
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Deployments.Handlers.Commands;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Deployments.Commands
{
    public class DeployMobileSuitHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly DeployMobileSuitHandler _handler;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public DeployMobileSuitHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new DeployMobileSuitHandler(_mockUnitOfWork.Object, _mapper, _user1HttpContext.Object);

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ValidDeployMobileSuitValues()
        {
            yield return new object[] { _user1HttpContext, 1 };
            yield return new object[] { _user1HttpContext, 2 };
            yield return new object[] { _user1HttpContext, 3 };
            yield return new object[] { _user2HttpContext, 4 };
            yield return new object[] { _user2HttpContext, 5 };
        }

        [Test]
        [TestCaseSource(nameof(ValidDeployMobileSuitValues))]
        public async Task Valid_DeployMobileSuit(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeployMobileSuitHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            var response = await handler.Handle(new DeployMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None);
            response.ShouldBeOfType<DeploymentDTO>();
            (response.ResultType == SD.GoodDeploymentResult || response.ResultType == SD.BadDeploymentResult).ShouldBeTrue();

            var userMobileSuit = await _mockUnitOfWork.Object.UserMobileSuitRepository.Get(userMobileSuitId);
            var userMobileSuitEquippedWeapon = await _mockUnitOfWork.Object.UserWeaponRepository.GetUserMobileSuitEquippedWeapon(userMobileSuitId);
            var userMobileSuitCustomWeapon = await _mockUnitOfWork.Object.UserWeaponRepository.GetUserMobileSuitCustomWeapon(userMobileSuitId);

            if (response.ResultType == SD.BadDeploymentResult)
            {                
                // If user had no insurance
                if (userMobileSuit.EndOfCoverage < DateTime.UtcNow)
                {
                    userMobileSuit.IsDamaged.ShouldBeTrue();
                    userMobileSuitEquippedWeapon.ShouldBeNull();
                    userMobileSuitCustomWeapon.ShouldBeNull();
                }
                // If user had insurance
                else
                {
                    if (userMobileSuit.InsurancePlan == SD.StandardInsurancePlan)
                    {
                        userMobileSuit.IsDamaged.ShouldBeFalse();
                        userMobileSuitEquippedWeapon.ShouldBeNull();
                        userMobileSuitCustomWeapon.ShouldBeNull();
                    }
                    else if (userMobileSuit.InsurancePlan == SD.SuperInsurancePlan)
                    {
                        userMobileSuit.IsDamaged.ShouldBeFalse();
                        userMobileSuitCustomWeapon.ShouldBeNull();
                    }
                    else if (userMobileSuit.InsurancePlan == SD.UltraInsurancePlan)
                    {
                        userMobileSuit.IsDamaged.ShouldBeFalse();
                    }
                }
            }
        }

        private static IEnumerable<object[]> NotFoundDeployMobileSuitValues()
        {
            yield return new object[] { _user1HttpContext, 234 };
            yield return new object[] { _user2HttpContext, -15 };
        }

        [Test]
        [TestCaseSource(nameof(NotFoundDeployMobileSuitValues))]
        public async Task DeployMobileSuit_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeployMobileSuitHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new DeployMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> UnauthorizedDeployMobileSuitValues()
        {
            yield return new object[] { _user1HttpContext, 4 };
            yield return new object[] { _user1HttpContext, 5 };
            yield return new object[] { _user2HttpContext, 1 };
            yield return new object[] { _user2HttpContext, 2 };
        }

        [Test]
        [TestCaseSource(nameof(UnauthorizedDeployMobileSuitValues))]
        public async Task DeployMobileSuit_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeployMobileSuitHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new DeployMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }

        private static IEnumerable<object[]> DamagedDeployMobileSuitValues()
        {
            yield return new object[] { _user2HttpContext, 6 };
        }

        [Test]
        [TestCaseSource(nameof(DamagedDeployMobileSuitValues))]
        public async Task DeployMobileSuit_ShouldThrow_MobileSuitDamagedException(Mock<IHttpContextAccessor> userHttpContext, int userMobileSuitId)
        {
            var handler = new DeployMobileSuitHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new DeployMobileSuitCommand() { UserMobileSuitId = userMobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<MobileSuitDamagedException>();
        }
    }
}
