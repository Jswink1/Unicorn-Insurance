using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.UnitTests.Mocks
{
    public class MockAutheticationService
    {
        public static Mock<IAuthenticationService> GetAuthenticationService()
        {
            //var deployments = new List<Deployment>()
            //{
            //    new Deployment
            //    {
            //        Id = 1,
            //        ResultType = SD.GoodDeploymentResult,
            //        ImageUrl = ""
            //    },
            //    new Deployment
            //    {
            //        Id = 2,
            //        ResultType = SD.GoodDeploymentResult,
            //        ImageUrl = ""
            //    },
            //    new Deployment
            //    {
            //        Id = 3,
            //        ResultType = SD.BadDeploymentResult,
            //        ImageUrl = ""
            //    }
            //};

            var mockRepo = new Mock<IAuthenticationService>();

            mockRepo.Setup(r => r.Authenticate(It.IsAny<LoginVM>())).ReturnsAsync(true);

            mockRepo.Setup(r => r.Register(It.IsAny<RegisterVM>())).ReturnsAsync(true);

            //mockRepo.Setup(r => r.GetDeploymentDetails(It.IsAny<int>())).ReturnsAsync((int deploymentId) =>
            //{
            //    var deployment = deployments.Where(d => d.Id == deploymentId).FirstOrDefault();

            //    return deployment;
            //});

            //mockRepo.Setup(r => r.UpdateDeployment(It.IsAny<Deployment>())).ReturnsAsync((Deployment deployment) =>
            //{
            //    var deploymentToUpdate = deployments.Where(d => d.Id == deployment.Id).FirstOrDefault();

            //    if (deploymentToUpdate is null)
            //        return new BaseCommandResponse() { Success = false, Message = "Failure" };
            //    else
            //        return new BaseCommandResponse() { Success = true, Message = "Success" };
            //});

            //mockRepo.Setup(r => r.InsertDeployment(It.IsAny<Deployment>())).ReturnsAsync((Deployment deployment) =>
            //{
            //    return new BaseCommandResponse() { Success = true, Message = "Success" };
            //});

            return mockRepo;
        }
    }
}
