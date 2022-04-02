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
using UnicornInsurance.Application.Contracts.Identity;
using UnicornInsurance.Application.DTOs.Order;
using UnicornInsurance.Application.DTOs.OrderDetails;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Handlers.Queries;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Orders.Queries
{
    public class GetOrderDetailsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user3HttpContext = new();
        private readonly Mock<IUserService> _userService = new();

        public GetOrderDetailsHandlerTests()
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

            _userService.Setup(x => x.GetUserRoles(It.IsAny<string>())).Returns((string userId) =>
            {
                var userRole = new List<string>();

                // user1 and user2 are customers
                if (userId == "user1" || userId == "user2")
                {
                    userRole = new List<string>() { SD.CustomerRole };

                    return Task.FromResult((IList<string>)userRole);
                }
                // user3 is an admin
                else if (userId == "user3")
                {
                    userRole = new List<string>() { SD.AdminRole };

                    return Task.FromResult((IList<string>)userRole);
                }

                return Task.FromResult((IList<string>)userRole);
            });
        }

        private static IEnumerable<object[]> GetOrderDetailsValues()
        {
            yield return new object[] { _user1HttpContext, 1 };
            yield return new object[] { _user2HttpContext, 2 };
        }

        [Test]
        [TestCaseSource(nameof(GetOrderDetailsValues))]
        public async Task GetOrderDetailsTest(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object, _userService.Object);

            var result = await handler.Handle(new GetOrderDetailsRequest() { OrderId = orderId }, CancellationToken.None);
            result.ShouldBeOfType<OrderDetailsDTO>();
            result.OrderHeader.ShouldNotBeNull();
        }

        private static IEnumerable<object[]> GetNotFoundOrderDetailsValues()
        {
            yield return new object[] { _user1HttpContext, 2342 };
            yield return new object[] { _user2HttpContext, -7 };
        }

        [Test]
        [TestCaseSource(nameof(GetNotFoundOrderDetailsValues))]
        public async Task GetOrderDetails_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object, _userService.Object);

            await handler.Handle(new GetOrderDetailsRequest() { OrderId = orderId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> GetUnauthorizedOrderDetailsValues()
        {
            yield return new object[] { _user1HttpContext, 2 };
            yield return new object[] { _user2HttpContext, 1 };
        }

        [Test]
        [TestCaseSource(nameof(GetUnauthorizedOrderDetailsValues))]
        public async Task GetOrderDetails_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object, _userService.Object);

            await handler.Handle(new GetOrderDetailsRequest() { OrderId = orderId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }

        private static IEnumerable<object[]> GetAdminOrderDetailsValues()
        {
            yield return new object[] { _user3HttpContext, 2 };
            yield return new object[] { _user3HttpContext, 1 };
        }

        [Test]
        [TestCaseSource(nameof(GetAdminOrderDetailsValues))]
        public async Task GetOrderDetails_AllowsAdminAccess_ToAnyOrder(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderDetailsHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object, _userService.Object);

            var result = await handler.Handle(new GetOrderDetailsRequest() { OrderId = orderId }, CancellationToken.None);
            result.ShouldBeOfType<OrderDetailsDTO>();
        }
    }
}
