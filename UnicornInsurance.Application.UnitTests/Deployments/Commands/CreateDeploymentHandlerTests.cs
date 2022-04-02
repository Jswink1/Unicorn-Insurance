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
using UnicornInsurance.Application.Features.Deployments.Handlers.Commands;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Deployments.Commands
{
    public class CreateDeploymentHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateDeploymentHandler _handler;

        public CreateDeploymentHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateDeploymentHandler(_mockUnitOfWork.Object, _mapper);
        }

        private static IEnumerable<object[]> ValidDeploymentValues()
        {
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    ResultType = "Bad",
                    Description = "Your Mobile Suit was destroyed by a BearGuy!"
                }
            };
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    ResultType = "Good",
                    Description = "Your Mobile Suit evaded an enemy barrage!"
                }
            };
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    ResultType = "Good",
                    Description = "You made friends with Haro!"
                }
            };
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    ResultType = "Bad",
                    Description = "Your Mobile Suit was shot to pieces!"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(ValidDeploymentValues))]
        public async Task Valid_CreateDeploymentTest(CreateDeploymentDTO createDeploymentDTO)
        {
            var deployments = await _mockUnitOfWork.Object.DeploymentRepository.GetAll();
            var count = deployments.Count;

            var result = await _handler.Handle(new CreateDeploymentCommand() { CreateDeploymentDTO = createDeploymentDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            deployments = await _mockUnitOfWork.Object.DeploymentRepository.GetAll();
            deployments.Count.ShouldBe(count + 1);
        }

        private static IEnumerable<object[]> InvalidDeploymentValues()
        {
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    ResultType = "Neutral",
                    Description = "Your Mobile Suit was destroyed by a BearGuy!"
                }
            };
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    ResultType = "Good"
                }
            };
            yield return new object[]
            {
                new CreateDeploymentDTO
                {
                    Description = "You made friends with Haro!"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(InvalidDeploymentValues))]
        public async Task Invalid_CreateDeploymentTest(CreateDeploymentDTO createDeploymentDTO)
        {
            var result = await _handler.Handle(new CreateDeploymentCommand() { CreateDeploymentDTO = createDeploymentDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }
    }
}
