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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Deployments.Handlers.Commands;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Deployments.Commands
{
    public class DeleteDeploymentHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteDeploymentHandler _handler;

        public DeleteDeploymentHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new DeleteDeploymentHandler(_mockUnitOfWork.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteDeploymentTest(int id)
        {
            await _handler.Handle(new DeleteDeploymentCommand() { DeploymentId = id }, CancellationToken.None);

            var deployment = await _mockUnitOfWork.Object.DeploymentRepository.Get(id);
            deployment.ShouldBeNull();
        }

        [Test]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(-300)]
        public async Task DeleteDeployment_ShouldThrow_NotFoundException(int id)
        {
            await _handler.Handle(new DeleteDeploymentCommand() { DeploymentId = id }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }
    }
}
