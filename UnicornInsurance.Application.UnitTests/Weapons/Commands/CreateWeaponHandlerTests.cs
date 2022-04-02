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
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Features.Weapons.Handlers.Commands;
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Weapons.Commands
{
    public class CreateWeaponHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateWeaponHandler _handler;
        private readonly CreateWeaponDTO _weaponDTO;

        public CreateWeaponHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateWeaponHandler(_mockUnitOfWork.Object, _mapper);

            _weaponDTO = new CreateWeaponDTO
            {
                Name = "Head-Mounted Vulcans",
                Description = "60mm Rotary Cannons mounted on the head of a Mobile Suit.",
                Price = 1100m
            };
        }

        private static IEnumerable<object[]> CreateWeaponValues()
        {
            yield return new object[] 
            { 
                new CreateWeaponDTO() 
                {
                    Name = "Random New Weapon",
                    Description = "Very random description",
                    Price = 1000m
                } 
            };
            yield return new object[]
            {
                new CreateWeaponDTO()
                {
                    Name = "Head-Mounted Vulcans",
                    Description = "60mm Rotary Cannons mounted on the head of a Mobile Suit.",
                    Price = 1100m
                }
            };
            yield return new object[]
            {
                new CreateWeaponDTO()
                {
                    Name = "Funnels",
                    Description = "Funnel-shaped drone units that are remotely controlled by a Newtype pilot.",
                    Price = 1999m
                }
            };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(CreateWeaponValues))]
        public async Task Valid_Weapon_Added(CreateWeaponDTO weapon)
        {
            var result = await _handler.Handle(new CreateWeaponCommand() { CreateWeaponDTO = weapon }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-234.52)]
        [TestCase(0)]
        [TestCase(163465)]
        public async Task Invalid_Weapon_Price(decimal price)
        {
            _weaponDTO.Price = price;

            var result = await _handler.Handle(new CreateWeaponCommand() { CreateWeaponDTO = _weaponDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public async Task Invalid_Weapon_Description(string description)
        {
            _weaponDTO.Description = description;

            var result = await _handler.Handle(new CreateWeaponCommand() { CreateWeaponDTO = _weaponDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public async Task Invalid_Weapon_Name(string name)
        {
            _weaponDTO.Name = name;

            var result = await _handler.Handle(new CreateWeaponCommand() { CreateWeaponDTO = _weaponDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }
    }
}
