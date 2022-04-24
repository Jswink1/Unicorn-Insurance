using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Contracts.Helpers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Models.ViewModels;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Controllers
{
    public class DeploymentsController : Controller
    {
        private readonly IDeploymentService _deploymentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IFileUploadHelper _fileUploadHelper;
        private readonly IBlobService _blobService;

        public DeploymentsController(IDeploymentService deploymentService,
                                     IWebHostEnvironment webHostEnvironment,
                                     IHttpContextHelper httpContextHelper,
                                     IFileUploadHelper fileUploadHelper,
                                     IBlobService blobService)
        {
            _deploymentService = deploymentService;
            _webHostEnvironment = webHostEnvironment;
            _httpContextHelper = httpContextHelper;
            _fileUploadHelper = fileUploadHelper;
            _blobService = blobService;
        }

        [Authorize(Roles = SD.AdminRole)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Upsert(int? id)
        {
            DeploymentUpsertVM model = new();

            // If inserting
            if (id == null)
            {
                return View(model);
            }

            // If updating
            else
            {
                model.Deployment = await _deploymentService.GetDeploymentDetails(id.GetValueOrDefault());

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Upsert(DeploymentUpsertVM model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            BaseCommandResponse response;

            // Get the web root path, and retrieve the file that has been uploaded
            var files = _httpContextHelper.GetUploadedFiles(this);

            // If an image file was uploaded
            if (files.Count > 0)
            {
                // If editing
                if (model.Deployment.ImageUrl != null)
                {
                    // Remove the old image
                    await _blobService.DeleteBlobAsync(model.Deployment.ImageUrl);
                }

                // Upload the image
                await _blobService.UploadFileBlobAsync((Microsoft.AspNetCore.Http.FormFile)files.First());

                model.Deployment.ImageUrl = "https://unicornblobstorage.blob.core.windows.net/images/" + files.First().FileName;
            }

            // Else, if the user did not upload a new image file
            else
            {
                // If editing, an image file for the product should already exist in the DB
                if (model.Deployment.Id != 0)
                {
                    // Retrieve the image stored in the DB
                    var deployment = await _deploymentService.GetDeploymentDetails(model.Deployment.Id);
                    model.Deployment.ImageUrl = deployment.ImageUrl;
                }
                // If inserting, the user needs to upload an image, so throw an error
                else
                {
                    TempData["Error"] = "You must upload an Image File";
                    return View(model);
                }
            }

            // If inserting
            if (model.Deployment.Id == 0)
            {
                response = await _deploymentService.InsertDeployment(model.Deployment);
            }
            // If updating
            else
            {
                response = await _deploymentService.UpdateDeployment(model.Deployment);
            }

            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response.Message;
                model.Errors = response.Errors;

                // If inserting
                if (model.Deployment.Id == 0)
                {
                    // Delete the image the user tried to upload
                    await _blobService.DeleteBlobAsync(model.Deployment.ImageUrl);
                }

                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Deploy(int id)
        {
            DeploymentVM model = new()
            {
                Deployment = await _deploymentService.DeployMobileSuit(id),
                UserMobileSuitId = id
            };

            return View(model);
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetDeploymentsList(string status)
        {
            var deployments = (IEnumerable<Deployment>)await _deploymentService.GetDeployments();

            switch (status)
            {
                case "good":
                    deployments = deployments.Where(o => o.ResultType == SD.GoodDeploymentResult);
                    break;
                case "bad":
                    deployments = deployments.Where(o => o.ResultType == SD.BadDeploymentResult);
                    break;
                default:
                    break;
            }

            return Json(new { data = deployments });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Remove the image
                var deployment = await _deploymentService.GetDeploymentDetails(id);

                await _blobService.DeleteBlobAsync(deployment.ImageUrl);

                await _deploymentService.DeleteDeployment(id);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}

