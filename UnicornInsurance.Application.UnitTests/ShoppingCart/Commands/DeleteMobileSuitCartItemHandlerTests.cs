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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.ShoppingCart.Commands
{
    public class DeleteMobileSuitCartItemHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteMobileSuitCartItemHandler _handler;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public DeleteMobileSuitCartItemHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new DeleteMobileSuitCartItemHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> ValidMobileSuitCartItems()
        {
            yield return new object[] { 1, _user1HttpContext };
            yield return new object[] { 2, _user1HttpContext };
            yield return new object[] { 3, _user2HttpContext };
        }

        [Test]
        [TestCaseSource(nameof(ValidMobileSuitCartItems))]
        public async Task Valid_MobileSuitCartItem_Delete(int mobileSuitCartItemId, Mock<IHttpContextAccessor> userHttpContext)
        {
            var handler = new DeleteMobileSuitCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteMobileSuitCartItemCommand() { MobileSuitCartItemId = mobileSuitCartItemId }, CancellationToken.None);

            var mobileSuitCartItem = await _mockUnitOfWork.Object.MobileSuitCartRepository.Get(mobileSuitCartItemId);
            mobileSuitCartItem.ShouldBeNull();
        }

        [Test]
        [TestCase(23)]
        [TestCase(23246)]
        [TestCase(-2)]
        public async Task DeleteMobileSuit_ShouldThrow_NotFoundException(int mobileSuitCartItemId)
        {
            await _handler.Handle(new DeleteMobileSuitCartItemCommand() { MobileSuitCartItemId = mobileSuitCartItemId }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }

        private static IEnumerable<object[]> UnauthorizedMobileSuitCartItems()
        {
            yield return new object[] { 3, _user1HttpContext };
            yield return new object[] { 1, _user2HttpContext };
            yield return new object[] { 2, _user2HttpContext };
        }

        [Test]
        [TestCaseSource(nameof(UnauthorizedMobileSuitCartItems))]
        public async Task DeleteMobileSuit_ShouldThrow_UnauthorizedAccessException(int mobileSuitCartItemId, Mock<IHttpContextAccessor> userHttpContext)
        {
            var handler = new DeleteMobileSuitCartItemHandler(_mockUnitOfWork.Object, userHttpContext.Object);

            await handler.Handle(new DeleteMobileSuitCartItemCommand() { MobileSuitCartItemId = mobileSuitCartItemId }, CancellationToken.None)
                         .ShouldThrowAsync<UnauthorizedAccessException>();
        }
    }
}
