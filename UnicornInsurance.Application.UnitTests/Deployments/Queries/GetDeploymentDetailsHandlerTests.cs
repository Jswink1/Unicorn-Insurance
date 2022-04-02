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
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Deployments.Handlers.Queries;
using UnicornInsurance.Application.Features.Deployments.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Deployments.Queries
{
    public class GetDeploymentDetailsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetDeploymentDetailsHandler _handler;

        public GetDeploymentDetailsHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetDeploymentDetailsHandler(_mockUnitOfWork.Object, _mapper);
        }

        private static IEnumerable<object[]> DeploymentDetailsValues()
        {
            yield return new object[] { 1, "You got two Mobile Suits with one shot!" };
            yield return new object[] { 2, "Your Mobile Suit got destroyed during the colony drop!" };
            yield return new object[] { 3, "Your Mobile Suit got damaged during the firing of the colony laser!" };
            yield return new object[] { 4, "Your Mobile Suit won a sword duel!" };
        }

        [Test]
        [TestCaseSource(nameof(DeploymentDetailsValues))]
        public async Task GetDeploymentDetailsTest(int id, string description)
        {
            var result = await _handler.Handle(new GetDeploymentDetailsRequest() { DeploymentId = id }, CancellationToken.None);

            result.ShouldBeOfType<DeploymentDTO>();
            result.Description.ShouldBe(description);
        }

        [Test]
        [TestCase(25)]
        [TestCase(500)]
        [TestCase(-4)]
        public async Task GetDeploymentDetails_ShouldThrow_NotFoundException(int id)
        {
            await _handler.Handle(new GetDeploymentDetailsRequest() { DeploymentId = id }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }
    }
}
