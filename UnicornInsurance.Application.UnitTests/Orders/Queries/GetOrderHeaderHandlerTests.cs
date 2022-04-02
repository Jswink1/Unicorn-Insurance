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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Handlers.Queries;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Orders.Queries
{
    public class GetOrderHeaderHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public GetOrderHeaderHandlerTests()
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
        }

        private static IEnumerable<object[]> GetOrderHeaderValues()
        {
            yield return new object[] { _user1HttpContext, 1 };
            yield return new object[] { _user2HttpContext, 2 };
        }

        [Test]
        [TestCaseSource(nameof(GetOrderHeaderValues))]
        public async Task GetOrderHeaderTest(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderHeaderHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            var result = await handler.Handle(new GetOrderHeaderRequest() { OrderId = orderId }, CancellationToken.None);
            result.ShouldBeOfType<OrderHeaderDTO>();
        }

        private static IEnumerable<object[]> GetNotFoundOrderHeaderValues()
        {
            yield return new object[] { _user1HttpContext, 234 };
            yield return new object[] { _user2HttpContext, -55 };
        }

        [Test]
        [TestCaseSource(nameof(GetNotFoundOrderHeaderValues))]
        public async Task GetOrderHeader_ShouldThrow_NotFoundException(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderHeaderHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new GetOrderHeaderRequest() { OrderId = orderId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> GetUnauthorizedOrderHeaderValues()
        {
            yield return new object[] { _user1HttpContext, 2 };
            yield return new object[] { _user2HttpContext, 1 };
        }

        [Test]
        [TestCaseSource(nameof(GetUnauthorizedOrderHeaderValues))]
        public async Task GetOrderHeader_ShouldThrow_UnauthorizedAccessException(Mock<IHttpContextAccessor> userHttpContext, int orderId)
        {
            var handler = new GetOrderHeaderHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            await handler.Handle(new GetOrderHeaderRequest() { OrderId = orderId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }
    }
}
