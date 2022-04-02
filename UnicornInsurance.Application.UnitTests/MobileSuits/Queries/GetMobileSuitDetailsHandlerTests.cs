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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.MobileSuits.Handlers.Queries;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.MobileSuits.Queries
{
    public class GetMobileSuitDetailsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetMobileSuitDetailsHandler _handler;

        public GetMobileSuitDetailsHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetMobileSuitDetailsHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Test]
        [TestCase(1, "RX-0 Unicorn Gundam")]
        [TestCase(2, "GN-001 Gundam Exia")]
        [TestCase(4, "ASW-G-08 Gundam Barbatos Lupus Rex")]
        [TestCase(5, "Sengoku Astray Gundam")]
        public async Task GetMobileSuitDetailsTest(int id, string mobileSuitName)
        {
            var result = await _handler.Handle(new GetMobileSuitDetailsRequest() { MobileSuitId = id }, CancellationToken.None);

            result.ShouldBeOfType<FullMobileSuitDTO>();
            result.Name.ShouldBe(mobileSuitName);
        }

        [Test]
        [TestCase(-12)]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(4543)]
        public async Task GetMobileSuitDetails_ShouldThrow_NotFoundException(int id)
        {           
            await _handler.Handle(new GetMobileSuitDetailsRequest() { MobileSuitId = id }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }
    }
}
