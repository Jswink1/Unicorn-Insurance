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
using UnicornInsurance.Application.Features.UserItems.Handlers.Queries;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.UserItems.Queries
{
    public class GetUserMobileSuitListHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user3HttpContext = new();

        public GetUserMobileSuitListHandlerTests()
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

        private static IEnumerable<object[]> GetUserMobileSuitListValues()
        {
            yield return new object[] { _user1HttpContext, 3 };
            yield return new object[] { _user2HttpContext, 3 };
        }

        [Test]
        [TestCaseSource(nameof(GetUserMobileSuitListValues))]
        public async Task GetUserMobileSuitListTest(Mock<IHttpContextAccessor> userHttpContext, int expectedCount)
        {
            var handler = new GetUserMobileSuitListHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            var result = await handler.Handle(new GetUserMobileSuitListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<UserMobileSuitDTO>>();
            result.Count.ShouldBe(expectedCount);
        }

        [Test]
        public async Task GetUserMobileSuitList_ShouldReturn_EmptyList()
        {
            var handler = new GetUserMobileSuitListHandler(_mockUnitOfWork.Object, _mapper, _user3HttpContext.Object);

            var result = await handler.Handle(new GetUserMobileSuitListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<UserMobileSuitDTO>>();
            result.Count.ShouldBe(0);
        }


    }
}
