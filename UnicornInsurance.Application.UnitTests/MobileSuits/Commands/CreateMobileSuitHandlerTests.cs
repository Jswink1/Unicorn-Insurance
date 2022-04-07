using AutoMapper;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.MobileSuits.Commands
{
    public class CreateMobileSuitHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateMobileSuitHandler _handler;
        private readonly CreateMobileSuitDTO _mobileSuitDTO;

        public CreateMobileSuitHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateMobileSuitHandler(_mockUnitOfWork.Object, _mapper);

            _mobileSuitDTO = new CreateMobileSuitDTO
            {
                Name = "MSN-00100 Hyaku Shiki",
                Description = "Originally designed as the prototype for a transformable mobile suit.",
                Price = 42000m
            };
        }

        private static IEnumerable<object[]> MobileSuitValues()
        {
            yield return new object[]
            {
                new CreateMobileSuitDTO()
                {
                    Name = "MSN-00100 Hyaku Shiki",
                    Description = "Originally designed as the prototype for a transformable mobile suit.",
                    Price = 42000m
                }
            };
            yield return new object[]
            {
                new CreateMobileSuitDTO()
                {
                    Name = "MSN-06S Sinanju",
                    Description = "Sporting multiple vernier thrusters throughout its frame",
                    Price = 44999m
                }
            };
        }

        private static IEnumerable<object[]> MobileSuitWithCustomWeaponValues()
        {
            yield return new object[]
            {
                new CreateMobileSuitDTO()
                {
                    Name = "GF13-017NJ Shining Gundam",
                    Description = "Designed with an emphasis on mobility. The Shining Gundam specializes in martial-arts fighting.",
                    Price = 41000m,
                    CustomWeapon = new CustomWeaponDTO()
                    {
                        Name = "Shining Finger",
                        Description = "Devastating Martial Arts Attack"
                    }
                }
            };
            yield return new object[]
            {
                new CreateMobileSuitDTO()
                {
                    Name = "RX-78-2 Gundam",
                    Description = "The original Gundam. Turned the tide of war in favor of the Earth Federation",
                    Price = 39999m,
                    CustomWeapon = new CustomWeaponDTO()
                    {
                        Name = "RX Shield",
                        Description = "Simple defensive equipment that can be carried or mounted to a Mobile Suit."
                    }
                }
            };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(MobileSuitValues))]
        public async Task Valid_MobileSuit_Added(CreateMobileSuitDTO mobileSuit)
        {
            var mobileSuitList = await _mockUnitOfWork.Object.MobileSuitRepository.GetAll();
            var count = mobileSuitList.Count;

            var result = await _handler.Handle(new CreateMobileSuitCommand() { CreateMobileSuitDTO = mobileSuit }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var mobileSuitDetails = await _mockUnitOfWork.Object.MobileSuitRepository.GetFullMobileSuitDetails(result.Id);
            mobileSuitDetails.CustomWeapon.ShouldBeNull();

            mobileSuitList = await _mockUnitOfWork.Object.MobileSuitRepository.GetAll();
            mobileSuitList.Count.ShouldBe(count + 1);
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(MobileSuitWithCustomWeaponValues))]
        public async Task Valid_MobileSuitWithCustomWeapon_Added(CreateMobileSuitDTO mobileSuit)
        {
            var mobileSuitList = await _mockUnitOfWork.Object.MobileSuitRepository.GetAll();
            var count = mobileSuitList.Count;

            var result = await _handler.Handle(new CreateMobileSuitCommand() { CreateMobileSuitDTO = mobileSuit }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var mobileSuitDetails = await _mockUnitOfWork.Object.MobileSuitRepository.GetFullMobileSuitDetails(result.Id);
            mobileSuitDetails.CustomWeapon.ShouldNotBeNull();
            mobileSuitDetails.CustomWeapon.IsCustomWeapon.ShouldBeTrue();

            mobileSuitList = await _mockUnitOfWork.Object.MobileSuitRepository.GetAll();
            mobileSuitList.Count.ShouldBe(count + 1);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-234.52)]
        [TestCase(0)]
        [TestCase(163465456)]
        public async Task Invalid_MobileSuit_Price(decimal price)
        {
            _mobileSuitDTO.Price = price;

            var result = await _handler.Handle(new CreateMobileSuitCommand() { CreateMobileSuitDTO = _mobileSuitDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public async Task Invalid_MobileSuit_Description(string description)
        {
            _mobileSuitDTO.Description = description;

            var result = await _handler.Handle(new CreateMobileSuitCommand() { CreateMobileSuitDTO = _mobileSuitDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public async Task Invalid_MobileSuit_Name(string name)
        {
            _mobileSuitDTO.Name = name;

            var result = await _handler.Handle(new CreateMobileSuitCommand() { CreateMobileSuitDTO = _mobileSuitDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }
    }
}
