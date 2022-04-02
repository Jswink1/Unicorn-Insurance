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
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Commands
{
    public class AddWeaponCartItemHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public AddWeaponCartItemHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> NewWeaponCartItems()
        {
            yield return new object[] { 3, _user1HttpContext, "user1" };
            yield return new object[] { 1, _user2HttpContext, "user2" };
            yield return new object[] { 3, _user2HttpContext, "user2" };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(NewWeaponCartItems))]
        public async Task New_WeaponCartItem_AddedToCart(int weaponId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new AddWeaponCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var userWeaponCartItems = await _mockUnitOfWork.Object.WeaponCartRepository.GetAllCartItems(userId);
            var count = userWeaponCartItems.Count;

            var result = await handler.Handle(new AddWeaponCartItemCommand() { WeaponId = weaponId }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
            result.Message.ShouldBe("Item Added to Cart");

            userWeaponCartItems = await _mockUnitOfWork.Object.WeaponCartRepository.GetAllCartItems(userId);
            userWeaponCartItems.Count.ShouldBe(count + 1);
        }

        private static IEnumerable<object[]> ExistingWeaponCartItems()
        {
            yield return new object[] { 1, _user1HttpContext, "user1" };
            yield return new object[] { 2, _user1HttpContext, "user1" };
            yield return new object[] { 2, _user2HttpContext, "user2" };
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(ExistingWeaponCartItems))]
        public async Task Existing_WeaponCartItem_AddedToCart(int weaponId, Mock<IHttpContextAccessor> userHttpContext, string userId)
        {
            var handler = new AddWeaponCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            var userWeaponCartItems = await _mockUnitOfWork.Object.WeaponCartRepository.GetAllCartItems(userId);
            var count = userWeaponCartItems.Count;

            var result = await handler.Handle(new AddWeaponCartItemCommand() { WeaponId = weaponId }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
            result.Message.ShouldBe("Cart Item Quantity Updated");

            userWeaponCartItems = await _mockUnitOfWork.Object.WeaponCartRepository.GetAllCartItems(userId);
            userWeaponCartItems.Count.ShouldBe(count);
        }

        [Test]
        [Order(5)]
        [TestCase(6)]
        [TestCase(-1)]
        [TestCase(234234)]
        public async Task Invalid_WeaponCartItem_AddedToCart(int weaponId)
        {
            var handler = new AddWeaponCartItemHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            await handler.Handle(new AddWeaponCartItemCommand() { WeaponId = weaponId }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }
    }
}
