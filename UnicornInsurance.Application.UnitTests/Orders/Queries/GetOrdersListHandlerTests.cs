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
using UnicornInsurance.Application.Features.Orders.Handlers.Queries;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Orders.Queries
{
    public class GetOrdersListHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user3HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user4HttpContext = new();
        private readonly Mock<IUserService> _userService = new();

        public GetOrdersListHandlerTests()
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
            _user4HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user4"));

            _userService.Setup(x => x.GetUserRoles(It.IsAny<string>())).Returns((string userId) =>
            {
                var userRole = new List<string>();

                // user1 and user2 and user3 are customers
                if (userId == "user1" || userId == "user2" || userId == "user3")
                {
                    userRole = new List<string>() { SD.CustomerRole };

                    return Task.FromResult((IList<string>)userRole);
                }
                // user3 is an admin
                else if (userId == "user4")
                {
                    userRole = new List<string>() { SD.AdminRole };

                    return Task.FromResult((IList<string>)userRole);
                }

                return Task.FromResult((IList<string>)userRole);
            });
        }

        private static IEnumerable<object[]> GetOrdersListValues()
        {
            yield return new object[] { _user1HttpContext, 1 };
            yield return new object[] { _user2HttpContext, 1 };
            yield return new object[] { _user4HttpContext, 2 };
        }

        [Test]
        [TestCaseSource(nameof(GetOrdersListValues))]
        public async Task GetOrdersListTest(Mock<IHttpContextAccessor> userHttpContext, int expectedCount)
        {
            var handler = new GetOrdersListHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object, _userService.Object);

            var result = await handler.Handle(new GetOrdersListRequest(), CancellationToken.None);
            result.ShouldBeOfType<List<OrderHeaderDTO>>();
            result.Count.ShouldBe(expectedCount);
        }

        private static IEnumerable<object[]> GetEmptyOrdersListValues()
        {
            yield return new object[] { _user3HttpContext, 0 };
        }

        [Test]
        [TestCaseSource(nameof(GetEmptyOrdersListValues))]
        public async Task GetOrdersList_ShouldReturn_EmptyList(Mock<IHttpContextAccessor> userHttpContext, int expectedCount)
        {
            var handler = new GetOrdersListHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object, _userService.Object);

            var result = await handler.Handle(new GetOrdersListRequest(), CancellationToken.None);
            result.ShouldBeOfType<List<OrderHeaderDTO>>();
            result.Count.ShouldBe(expectedCount);
        }
    }
}
