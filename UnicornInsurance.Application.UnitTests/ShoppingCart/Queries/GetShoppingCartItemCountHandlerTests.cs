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
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Queries;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Queries;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Queries
{
    public class GetShoppingCartItemCountHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public GetShoppingCartItemCountHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ShoppingCartItemCountValues()
        {
            yield return new object[] { _user1HttpContext, 4 };
            yield return new object[] { _user2HttpContext, 2 };
        }

        [Test]
        [TestCaseSource(nameof(ShoppingCartItemCountValues))]
        public async Task GetShoppingCartItemCountTest(Mock<IHttpContextAccessor> userHttpContext, int expectedCount)
        {
            var handler = new GetShoppingCartItemCountHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var result = await handler.Handle(new GetShoppingCartItemCountRequest(), CancellationToken.None);

            result.ShouldBeOfType<ShoppingCartItemCountResponse>();
            result.ShoppingCartItemCount.ShouldBe(expectedCount);
        }
    }
}
