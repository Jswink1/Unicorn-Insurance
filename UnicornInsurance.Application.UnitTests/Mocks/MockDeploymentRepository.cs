using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.UnitTests.Mocks
{
    public static class MockDeploymentRepository
    {
        public static Mock<IDeploymentRepository> GetDeploymentRepository()
        {
            var deployments = new List<Deployment>
            {
                new Deployment
                {
                    Id = 1,
                    ResultType = "Good",
                    Description = "You got two Mobile Suits with one shot!"
                },
                new Deployment
                {
                    Id = 2,
                    ResultType = "Bad",
                    Description = "Your Mobile Suit got destroyed during the colony drop!"
                },
                new Deployment
                {
                    Id = 3,
                    ResultType = "Bad",
                    Description = "Your Mobile Suit got damaged during the firing of the colony laser!"
                },
                new Deployment
                {
                    Id = 4,
                    ResultType = "Good",
                    Description = "Your Mobile Suit won a sword duel!"
                },
            };

            var mockRepo = new Mock<IDeploymentRepository>();

            mockRepo.Setup(r => r.Add(It.IsAny<Deployment>())).ReturnsAsync((Deployment deployment) =>
            {
                deployment.Id = deployments.Last().Id + 1;

                deployments.Add(deployment);

                return deployment;
            });

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(() =>
            {
                return deployments;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int deploymentId) =>
            {
                var deployment = deployments.Where(d => d.Id == deploymentId)
                                            .FirstOrDefault();

                return deployment;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<Deployment>())).Returns((Deployment deployment) =>
            {
                var deploymentToDelete = deployments.Where(d => d.Id == deployment.Id)
                                                    .FirstOrDefault();

                deployments.Remove(deploymentToDelete);

                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.GetGoodDeploymentResults()).ReturnsAsync(() =>
            {
                var goodDeployments = deployments.Where(d => d.ResultType == SD.GoodDeploymentResult)
                                                 .ToList();

                return goodDeployments;
            });

            mockRepo.Setup(r => r.GetBadDeploymentResults()).ReturnsAsync(() =>
            {
                var badDeployments = deployments.Where(d => d.ResultType == SD.BadDeploymentResult)
                                                .ToList();

                return badDeployments;
            });

            return mockRepo;
        }
    }
}
