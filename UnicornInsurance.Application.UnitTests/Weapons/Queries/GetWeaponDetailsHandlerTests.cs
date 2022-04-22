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
using UnicornInsurance.Application.Features.Weapons.Handlers.Queries;
using UnicornInsurance.Application.Features.Weapons.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Weapons.Queries
{
    public class GetWeaponDetailsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetWeaponDetailsHandler _handler;

        public GetWeaponDetailsHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetWeaponDetailsHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Test]
        [TestCase(1, "Beam Rifle")]
        [TestCase(2, "Beam Spray Gun")]
        [TestCase(3, "Beam Saber")]
        public async Task GetWeaponDetailsTest(int id, string weaponName)
        {
            var result = await _handler.Handle(new GetWeaponDetailsRequest() { WeaponId = id }, CancellationToken.None);

            result.ShouldBeOfType<WeaponDTO>();
            result.Name.ShouldBe(weaponName);
        }

        [Test]
        [TestCase(-12)]
        [TestCase(0)]
        [TestCase(4)]
        [TestCase(4543)]
        public async Task GetWeaponDetails_ShouldThrow_NotFoundException(int id)
        {
            await _handler.Handle(new GetWeaponDetailsRequest() { WeaponId = id }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }

        [Test]
        [TestCase(6)]
        public async Task GetWeaponDetails_ShouldThrow_CustomWeaponDetailsException(int id)
        {
            await _handler.Handle(new GetWeaponDetailsRequest() { WeaponId = id }, CancellationToken.None)
                          .ShouldThrowAsync<CustomWeaponDetailsException>();
        }
    }
}
