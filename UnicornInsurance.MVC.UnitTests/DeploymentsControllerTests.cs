using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Contracts.Helpers;
using UnicornInsurance.MVC.Controllers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Models.ViewModels;
using UnicornInsurance.MVC.UnitTests.Mocks;
using Xunit;

namespace UnicornInsurance.MVC.UnitTests
{
    public class DeploymentsControllerTests
    {
        private Mock<IDeploymentService> _deploymentService;
        private Mock<IWebHostEnvironment> _webHostEnvironment;
        private Mock<IHttpContextHelper> _httpContextHelper;
        private Mock<IFileUploadHelper> _fileUploadHelper;
        private DeploymentsController _deploymentsController;
        private DeploymentUpsertVM _deploymentUpsertVM;

        public DeploymentsControllerTests()
        {
            _deploymentService = MockDeploymentService.GetDeploymentService();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _httpContextHelper = new Mock<IHttpContextHelper>();
            _fileUploadHelper = new Mock<IFileUploadHelper>();

            _webHostEnvironment.Setup(x => x.WebRootPath).Returns("wwwroot");

            _deploymentsController = new DeploymentsController(_deploymentService.Object, 
                                                               _webHostEnvironment.Object,
                                                               _httpContextHelper.Object,
                                                               _fileUploadHelper.Object);

            _deploymentUpsertVM = new DeploymentUpsertVM()
            {
                Deployment = new Deployment()
                {
                    Description = "Random Description",
                    ImageUrl = "",
                    ResultType = SD.BadDeploymentResult
                }
            };
        }

        [Theory]
        [InlineData(null, 3)]
        [InlineData("good", 2)]
        [InlineData("bad", 1)]
        [InlineData("random string", 3)]
        public async Task GetDeploymentsListTest(string status, int expectedCount)
        {
            var result = await _deploymentsController.GetDeploymentsList(status);

            var jsonResult = Assert.IsType<JsonResult>(result);

            var deployments = jsonResult.Value.GetType().GetProperty("data")?.GetValue(jsonResult.Value, null) as IEnumerable<Deployment>;

            deployments.ToList().Count.ShouldBe(expectedCount);

            // GetDeployments() should only be called once
            _deploymentService.Verify(x => x.GetDeployments(), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpsertPage_ShouldDisplay_UpdateDeploymentPage(int deploymentId)
        {
            var result = await _deploymentsController.Upsert(deploymentId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<DeploymentUpsertVM>(viewResult.ViewData.Model);

            model.Deployment.Id.ShouldBe(deploymentId);

            // GetDeploymentDetails() should only be called once
            _deploymentService.Verify(x => x.GetDeploymentDetails(deploymentId), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        public async Task UpsertPage_ShouldDisplay_CreateDeploymentPage(int? deploymentId)
        {
            var result = await _deploymentsController.Upsert(deploymentId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<DeploymentUpsertVM>(viewResult.ViewData.Model);

            model.Deployment.Id.ShouldBe(0);
            model.Deployment.Description.ShouldBeNull();

            // GetDeploymentDetails() should not be called
            _deploymentService.Verify(x => x.GetDeploymentDetails(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpsertPage_Update_Test(int deploymentId)
        {
            _httpContextHelper.Setup(x => x.GetUploadedFiles(It.IsAny<ControllerBase>()))
                                .Returns(new FormFileCollection());

            _deploymentUpsertVM.Deployment.Id = deploymentId;

            var result = await _deploymentsController.Upsert(_deploymentUpsertVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // UpdateDeployment() should only be called
            _deploymentService.Verify(x => x.UpdateDeployment(It.IsAny<Deployment>()), Times.Once);
            _deploymentService.Verify(x => x.InsertDeployment(It.IsAny<Deployment>()), Times.Never);
        }

        [Fact]
        public async Task UpsertPage_Create_Test()
        {
            _httpContextHelper.Setup(x => x.GetUploadedFiles(It.IsAny<ControllerBase>()))
                                .Returns(new FormFileCollection()
                                {
                                    new FormFile(It.IsAny<Stream>(),
                                                 It.IsAny<long>(),
                                                 It.IsAny<long>(),
                                                 It.IsAny<string>(),
                                                 "image.webp")
                                });

            _fileUploadHelper.Setup(x => x.UploadImageFile(It.IsAny<IFormFileCollection>(),
                                                           It.IsAny<string>(),
                                                           It.IsAny<string>(),
                                                           It.IsAny<string>()));

            var result = await _deploymentsController.Upsert(_deploymentUpsertVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe(null);
            redirectToActionResult.ActionName.ShouldBe("Index");

            // InsertDeployment() should only be called
            _deploymentService.Verify(x => x.UpdateDeployment(It.IsAny<Deployment>()), Times.Never);
            _deploymentService.Verify(x => x.InsertDeployment(It.IsAny<Deployment>()), Times.Once);
        }

        [Fact]
        public async Task UpsertPage_ReturnsViewResult_WhenModelIsInvalid()
        {
            _deploymentsController.ModelState.AddModelError("Name", "Required");

            var result = await _deploymentsController.Upsert(_deploymentUpsertVM);

            var viewResult = Assert.IsType<ViewResult>(result);

            // UpdateDeployment() and InsertDeployment() should not be called
            _deploymentService.Verify(x => x.UpdateDeployment(It.IsAny<Deployment>()), Times.Never);
            _deploymentService.Verify(x => x.InsertDeployment(It.IsAny<Deployment>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DeleteDeploymentTest(int deploymentId)
        {
            var result = await _deploymentsController.Delete(deploymentId);

            var jsonResult = Assert.IsType<JsonResult>(result);

            var success = jsonResult.Value.GetType().GetProperty("success")?.GetValue(jsonResult.Value, null) as bool?;

            success.ShouldBe(true);

            // DeleteWeapon() should only be called once
            _deploymentService.Verify(x => x.DeleteDeployment(It.IsAny<int>()), Times.Once);
        }
    }
}
