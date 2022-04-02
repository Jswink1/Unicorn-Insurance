﻿using AutoMapper;
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
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Queries;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Queries
{
    public class GetWeaponCartItemListHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public GetWeaponCartItemListHandlerTests()
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

        private static IEnumerable<object[]> GetWeaponCartItemListValues()
        {
            yield return new object[] { _user1HttpContext, 2 };
            yield return new object[] { _user2HttpContext, 1 };
        }

        [Test]
        [TestCaseSource(nameof(GetWeaponCartItemListValues))]
        public async Task GetWeaponCartItemListTest(Mock<IHttpContextAccessor> userHttpContext, int expectedCount)
        {
            var handler = new GetWeaponCartItemListHandler(_mockUnitOfWork.Object, _mapper, userHttpContext.Object);

            var result = await handler.Handle(new GetWeaponCartItemListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<WeaponCartItemDTO>>();

            result.Count.ShouldBe(expectedCount);
        }
    }
}
