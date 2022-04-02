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
using UnicornInsurance.Application.DTOs.Order;
using UnicornInsurance.Application.DTOs.OrderDetails;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Handlers.Commands;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Orders.Commands
{
    public class InitializeOrderHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly InitializeOrderHandler _handler;
        private readonly static Mock<IHttpContextAccessor> _userHttpContext = new();

        public InitializeOrderHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new InitializeOrderHandler(_mockUnitOfWork.Object, _userHttpContext.Object);

            _userHttpContext.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<Predicate<Claim>>()))
                                .Returns(new Claim(SD.Uid, "user1"));
        }

        private static IEnumerable<object[]> GetInitializeOrderValues()
        {
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    MobileSuitPurchases = new List<CreateMobileSuitPurchaseDTO>()
                    {
                        new CreateMobileSuitPurchaseDTO { MobileSuitId = 1 },
                        new CreateMobileSuitPurchaseDTO { MobileSuitId = 2 }
                    }
                }
            };
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>()
                    {
                        new CreateWeaponPurchaseDTO { WeaponId = 1, Count = 2 },
                        new CreateWeaponPurchaseDTO { WeaponId = 2, Count = 1 }
                    }
                }
            };
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    MobileSuitPurchases = new List<CreateMobileSuitPurchaseDTO>()
                    {
                        new CreateMobileSuitPurchaseDTO { MobileSuitId = 4 }
                    },
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>()
                    {
                        new CreateWeaponPurchaseDTO { WeaponId = 3, Count = 2 }
                    }
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(GetInitializeOrderValues))]
        public async Task Valid_InitializeOrder(InitializeOrderDTO InitializeOrderDTO)
        {
            var orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            var count = orderHeaders.Count;

            var result = await _handler.Handle(new InitializeOrderCommand() { InitializeOrderDTO = InitializeOrderDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            orderHeaders.Count.ShouldBe(count + 1);            
        }

        private static IEnumerable<object[]> GetInvalidInitializeOrderValues()
        {
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    MobileSuitPurchases = new List<CreateMobileSuitPurchaseDTO>() { },
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>() { }
                }
            };
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    MobileSuitPurchases = new List<CreateMobileSuitPurchaseDTO>() { new CreateMobileSuitPurchaseDTO { } },
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>() { new CreateWeaponPurchaseDTO { } }
                }
            };
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>()
                    {
                        new CreateWeaponPurchaseDTO { WeaponId = 1, Count = 0 }
                    }
                }
            };
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>()
                    {
                        new CreateWeaponPurchaseDTO { WeaponId = 1, Count = -32 }
                    }
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(GetInvalidInitializeOrderValues))]
        public async Task Invalid_InitializeOrder(InitializeOrderDTO InitializeOrderDTO)
        {
            var orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            var count = orderHeaders.Count;

            var result = await _handler.Handle(new InitializeOrderCommand() { InitializeOrderDTO = InitializeOrderDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            orderHeaders.Count.ShouldBe(count);
        }

        private static IEnumerable<object[]> GetNotFoundInitializeOrderValues()
        {
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    MobileSuitPurchases = new List<CreateMobileSuitPurchaseDTO>() 
                    { 
                        new CreateMobileSuitPurchaseDTO { MobileSuitId = 56 } 
                    }
                }
            };
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>() 
                    { 
                        new CreateWeaponPurchaseDTO { WeaponId = 23, Count = 2 }
                    }
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(GetNotFoundInitializeOrderValues))]
        public async Task InitializeOrder_ShouldThrow_NotFoundException(InitializeOrderDTO InitializeOrderDTO)
        {
            var orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            var count = orderHeaders.Count;

            await _handler.Handle(new InitializeOrderCommand() { InitializeOrderDTO = InitializeOrderDTO }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();

            orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            orderHeaders.Count.ShouldBe(count);
        }

        private static IEnumerable<object[]> GetCustomWeaponInitializeOrderValues()
        {
            yield return new object[]
            {
                new InitializeOrderDTO
                {
                    WeaponPurchases = new List<CreateWeaponPurchaseDTO>() 
                    { 
                        new CreateWeaponPurchaseDTO { WeaponId = 6, Count = 2 } 
                    }
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(GetCustomWeaponInitializeOrderValues))]
        public async Task InitializeOrder_ShouldThrow_PurchaseCustomWeaponException(InitializeOrderDTO InitializeOrderDTO)
        {
            var orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            var count = orderHeaders.Count;

            await _handler.Handle(new InitializeOrderCommand() { InitializeOrderDTO = InitializeOrderDTO }, CancellationToken.None)
                          .ShouldThrowAsync<PurchaseCustomWeaponException>();

            orderHeaders = await _mockUnitOfWork.Object.OrderHeaderRepository.GetUserOrders("user1");
            orderHeaders.Count.ShouldBe(count);
        }
    }
}
