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
using UnicornInsurance.Application.Features.MobileSuits.Handlers.Queries;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.MobileSuits.Queries
{
    public class GetMobileSuitListHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public GetMobileSuitListHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public async Task GetMobileSuitListTest()
        {
            var handler = new GetMobileSuitListHandler(_mockUnitOfWork.Object, _mapper);

            var result = await handler.Handle(new GetMobileSuitListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<MobileSuitDTO>>();
            result.Count.ShouldBe(4);
        }
    }
}
