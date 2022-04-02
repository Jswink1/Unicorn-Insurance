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
using UnicornInsurance.Application.Features.Deployments.Handlers.Queries;
using UnicornInsurance.Application.Features.Deployments.Requests.Queries;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Deployments.Queries
{
    public class GetDeploymentListHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public GetDeploymentListHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public async Task GetDeploymentListTest()
        {
            var handler = new GetDeploymentListHandler(_mockUnitOfWork.Object, _mapper);

            var result = await handler.Handle(new GetDeploymentListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<DeploymentDTO>>();
            result.Count.ShouldBe(4);
        }
    }
}
