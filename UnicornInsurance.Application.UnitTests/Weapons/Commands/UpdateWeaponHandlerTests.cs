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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Weapons.Handlers.Commands;
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Weapons.Commands
{
    public class UpdateWeaponHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateWeaponHandler _handler;

        public UpdateWeaponHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateWeaponHandler(_mockUnitOfWork.Object, _mapper);
        }

        private static IEnumerable<object[]> UpdateWeaponValues()
        {
            yield return new object[]
            {
                new WeaponDTO()
                {
                    Id = 1,
                    Name = "Super Beam Rifle",
                    Description = "Better than the a regular Beam Rifle.",
                    Price = 1850m
                }
            };
            yield return new object[]
            {
                new WeaponDTO()
                {
                    Id = 2,
                    Name = "Super Beam Spray Gun",
                    Description = "Better than the a regular Beam Spray Gun.",
                    Price = 1750m
                }
            };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(UpdateWeaponValues))]
        public async Task Valid_Weapon_Updated(WeaponDTO weapon)
        {
            var result = await _handler.Handle(new UpdateWeaponCommand() { WeaponDTO = weapon }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(UpdateWeaponValues))]
        public async Task UpdateWeapon_ShouldThrow_NotFoundException(WeaponDTO weapon)
        {
            Random rnd = new();

            weapon.Id = rnd.Next(100, 1000000);

            await _handler.Handle(new UpdateWeaponCommand() { WeaponDTO = weapon }, CancellationToken.None)
                            .ShouldThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task UpdateCustomWeapon_ShouldThrow_UpdateCustomWeaponException()
        {
            WeaponDTO CustomWeaponDTO = new()
            {
                Id = 6,
                Name = "Unicorn Custom Weapon",
                Description = "I am trying to update a custom weapon",
                Price = 1750m
            };

            await _handler.Handle(new UpdateWeaponCommand() { WeaponDTO = CustomWeaponDTO }, CancellationToken.None)
                          .ShouldThrowAsync<UpdateCustomWeaponException>();
        }
    }
}
