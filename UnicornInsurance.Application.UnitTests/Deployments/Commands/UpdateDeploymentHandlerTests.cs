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
using UnicornInsurance.Application.Features.Deployments.Handlers.Commands;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Deployments.Commands
{
    public class UpdateDeploymentHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateDeploymentHandler _handler;

        public UpdateDeploymentHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateDeploymentHandler(_mockUnitOfWork.Object, _mapper);
        }

        private static IEnumerable<object[]> ValidDeploymentValues()
        {
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 1,
                    ResultType = "Bad",
                    Description = "Random Description!"
                }                
            };
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 2,
                    ResultType = "Good",
                    Description = "Super Random Description!"
                }
            };
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 3,
                    ResultType = "Bad",
                    Description = "Your Mobile Suit was struck by lightning!"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(ValidDeploymentValues))]
        public async Task Valid_UpdateDeploymentTest(DeploymentDTO deploymentDTO)
        {
            var result = await _handler.Handle(new UpdateDeploymentCommand() { DeploymentDTO = deploymentDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var deployment = await _mockUnitOfWork.Object.DeploymentRepository.Get(deploymentDTO.Id);
            deployment.Description.ShouldBe(deploymentDTO.Description);
        }

        private static IEnumerable<object[]> InvalidDeploymentValues()
        {
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 1,
                    Description = "Your Mobile Suit was struck by lightning!"
                }
            };
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 2,
                    ResultType = "Good",
                }
            };
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 3,
                    ResultType = "Random Result",
                    Description = "Your Mobile Suit was struck by lightning!"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(InvalidDeploymentValues))]
        public async Task Invalid_UpdateDeploymentTest(DeploymentDTO deploymentDTO)
        {
            var result = await _handler.Handle(new UpdateDeploymentCommand() { DeploymentDTO = deploymentDTO }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var deployment = await _mockUnitOfWork.Object.DeploymentRepository.Get(deploymentDTO.Id);
            deployment.Description.ShouldNotBe(deploymentDTO.Description);
        }

        private static IEnumerable<object[]> DeploymentNotFoundValues()
        {
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = -3,
                    ResultType = "Good",
                    Description = "Your Mobile Suit was struck by lightning!"
                }
            };
            yield return new object[]
            {
                new DeploymentDTO
                {
                    Id = 123,
                    ResultType = "Bad",
                    Description = "Your Mobile Suit was struck by lightning!"
                }
            };
        }

        [Test]
        [TestCaseSource(nameof(DeploymentNotFoundValues))]
        public async Task UpdateDeploymentTest_ShouldThrow_NotFoundException(DeploymentDTO deploymentDTO)
        {
            await _handler.Handle(new UpdateDeploymentCommand() { DeploymentDTO = deploymentDTO }, CancellationToken.None)
                         .ShouldThrowAsync<NotFoundException>();
        }
    }
}
