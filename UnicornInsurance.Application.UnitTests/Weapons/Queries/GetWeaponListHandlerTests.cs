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
using UnicornInsurance.Application.Features.Weapons.Handlers.Queries;
using UnicornInsurance.Application.Features.Weapons.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Weapons.Queries
{
    public class GetWeaponListHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public GetWeaponListHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public async Task GetWeaponListTest()
        {
            var handler = new GetWeaponListHandler(_mockUnitOfWork.Object, _mapper);

            var result = await handler.Handle(new GetWeaponListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<WeaponDTO>>();
            result.Count.ShouldBe(3);
        }
    }
}
