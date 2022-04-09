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
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Commands
{
    public class AddMobileSuitCartItemHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public AddMobileSuitCartItemHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> NewMobileSuitCartItems()
        {
            yield return new object[] { 2, _user2HttpContext, "user2" };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(NewMobileSuitCartItems))]
        public async Task New_MobileSuitCartItem_AddedToCart(int mobileSuitId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new AddMobileSuitCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var userMobileSuitCartItems = await _mockUnitOfWork.Object.MobileSuitCartRepository.GetAllCartItems(userId);
            var count = userMobileSuitCartItems.Count;

            var result = await handler.Handle(new AddMobileSuitCartItemCommand() { MobileSuitId = mobileSuitId }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
            result.Message.ShouldBe("Item Added to Cart");

            userMobileSuitCartItems = await _mockUnitOfWork.Object.MobileSuitCartRepository.GetAllCartItems(userId);
            userMobileSuitCartItems.Count.ShouldBe(count + 1);
        }

        private static IEnumerable<object[]> ExistingMobileSuitCartItems()
        {
            yield return new object[] { 2, _user1HttpContext, "user1" };
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(ExistingMobileSuitCartItems))]
        public async Task ExistingMobileSuitCartItem_AddToCartAttempt_ForUser1(int mobileSuitId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new AddMobileSuitCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var userMobileSuitCartItems = await _mockUnitOfWork.Object.MobileSuitCartRepository.GetAllCartItems(userId);
            var count = userMobileSuitCartItems.Count;

            var result = await handler.Handle(new AddMobileSuitCartItemCommand() { MobileSuitId = mobileSuitId }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("User can only have one of each type of mobile suit");

            userMobileSuitCartItems = await _mockUnitOfWork.Object.MobileSuitCartRepository.GetAllCartItems(userId);
            userMobileSuitCartItems.Count.ShouldBe(count);
        }

        [Test]
        [Order(3)]
        [TestCase(6)]
        [TestCase(-14)]
        [TestCase(234234)]
        public async Task Invalid_MobileSuitCartItem_AddedToCart(int mobileSuitId)
        {
            var handler = new AddMobileSuitCartItemHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            await handler.Handle(new AddMobileSuitCartItemCommand() { MobileSuitId = mobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }

        [Test]
        [Order(4)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task AddMobileSuitCartItem_ShouldThrow_SingleMobileSuitException(int mobileSuitId)
        {
            var handler = new AddMobileSuitCartItemHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            await handler.Handle(new AddMobileSuitCartItemCommand() { MobileSuitId = mobileSuitId }, CancellationToken.None)
                         .ShouldThrowAsync<SingleMobileSuitException>();
        }
    }
}
