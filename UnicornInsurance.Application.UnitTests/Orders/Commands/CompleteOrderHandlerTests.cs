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
using UnicornInsurance.Application.DTOs.Order;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Handlers.Commands;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Orders.Commands
{
    public class CompleteOrderHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CompleteOrderHandler _handler;
        private readonly static Mock<IHttpContextAccessor> _user1HttpContext = new();
        private readonly static Mock<IHttpContextAccessor> _user2HttpContext = new();

        public CompleteOrderHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new CompleteOrderHandler(_mockUnitOfWork.Object, _user1HttpContext.Object);

            _user1HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
            _user2HttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user2"));
        }

        private static IEnumerable<object[]> GetApprovedOrderValues()
        {
            yield return new object[]
            {
                new CompleteOrderDTO
                {
                    OrderId = 1,
                    TransactionSuccess = true,
                    TransactionId = "1"
                }
            };
        }

        [Test]
        [Order(3)]
        [TestCaseSource(nameof(GetApprovedOrderValues))]
        public async Task Valid_CompleteOrder_PaymentAccepted(CompleteOrderDTO completeOrderDTO)
        {
            var result = await _handler.Handle(new CompleteOrderCommand() { CompleteOrderDTO = completeOrderDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var orderHeader = await _mockUnitOfWork.Object.OrderHeaderRepository.Get(completeOrderDTO.OrderId);
            orderHeader.PaymentStatus.ShouldBe(SD.PaymentStatusApproved);

            var userMobileSuits = await _mockUnitOfWork.Object.UserMobileSuitRepository.GetAllUserMobileSuits("user1");
            userMobileSuits.Count.ShouldBe(4);
            var userWeapons = await _mockUnitOfWork.Object.UserWeaponRepository.GetAvailableUserWeapons("user1");
            userWeapons.Count.ShouldBe(2);
        }

        private static IEnumerable<object[]> GetRejectedOrderValues()
        {
            yield return new object[]
            {
                new CompleteOrderDTO
                {
                    OrderId = 1,
                    TransactionSuccess = false,
                    TransactionId = "1"
                }
            };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(GetRejectedOrderValues))]
        public async Task Valid_CompleteOrder_PaymentRejected(CompleteOrderDTO completeOrderDTO)
        {
            var result = await _handler.Handle(new CompleteOrderCommand() { CompleteOrderDTO = completeOrderDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var orderHeader = await _mockUnitOfWork.Object.OrderHeaderRepository.Get(completeOrderDTO.OrderId);
            orderHeader.PaymentStatus.ShouldBe(SD.PaymentStatusRejected);

            var userMobileSuits = await _mockUnitOfWork.Object.UserMobileSuitRepository.GetAllUserMobileSuits("user1");
            userMobileSuits.Count.ShouldBe(3);
            var userWeapons = await _mockUnitOfWork.Object.UserWeaponRepository.GetAvailableUserWeapons("user1");
            userWeapons.Count.ShouldBe(1);
        }

        private static IEnumerable<object[]> GetInvalidOrderValues()
        {
            yield return new object[]
            {
                new CompleteOrderDTO
                {
                    TransactionSuccess = true,
                    TransactionId = "1"
                }                
            };
        }

        [Test]
        [TestCaseSource(nameof(GetInvalidOrderValues))]
        public async Task Invalid_CompleteOrder(CompleteOrderDTO completeOrderDTO)
        {
            var result = await _handler.Handle(new CompleteOrderCommand() { CompleteOrderDTO = completeOrderDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }

        private static IEnumerable<object[]> GetNotFoundOrderValues()
        {
            yield return new object[]
            {
                new CompleteOrderDTO
                {
                    OrderId = 254,
                    TransactionSuccess = true,
                    TransactionId = "1"
                }
            };
            yield return new object[]
            {
                new CompleteOrderDTO
                {
                    OrderId = -3,
                    TransactionSuccess = true,
                    TransactionId = "1"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(GetNotFoundOrderValues))]
        public async Task CompleteOrder_ShouldThrow_NotFoundException(CompleteOrderDTO completeOrderDTO)
        {
            await _handler.Handle(new CompleteOrderCommand() { CompleteOrderDTO = completeOrderDTO }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(GetApprovedOrderValues))]
        public async Task CompleteOrder_ShouldThrow_UnauthorizedAccessException(CompleteOrderDTO completeOrderDTO)
        {
            var handler = new CompleteOrderHandler(_mockUnitOfWork.Object, _user2HttpContext.Object);

            await handler.Handle(new CompleteOrderCommand() { CompleteOrderDTO = completeOrderDTO }, CancellationToken.None)
                          .ShouldThrowAsync<UnauthorizedAccessException>();
        }

        private static IEnumerable<object[]> GetAlreadyApprovedOrderValues()
        {
            yield return new object[]
            {
                new CompleteOrderDTO
                {
                    OrderId = 2,
                    TransactionSuccess = true,
                    TransactionId = "1"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(GetAlreadyApprovedOrderValues))]
        public async Task CompleteOrder_ShouldThrow_PaymentAlreadyApprovedException(CompleteOrderDTO completeOrderDTO)
        {
            await _handler.Handle(new CompleteOrderCommand() { CompleteOrderDTO = completeOrderDTO }, CancellationToken.None)
                          .ShouldThrowAsync<PaymentAlreadyApprovedException>();
        }
    }
}
