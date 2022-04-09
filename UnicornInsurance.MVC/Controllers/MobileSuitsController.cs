using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Contracts.Helpers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Models.ViewModels;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Controllers
{
    public class MobileSuitsController : Controller
    {
        private readonly IMobileSuitService _mobileSuitService;
        private readonly IWeaponService _weaponService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IFileUploadHelper _fileUploadHelper;

        public MobileSuitsController(IMobileSuitService mobileSuitService,
                                     IWeaponService weaponService,
                                     IWebHostEnvironment webHostEnvironment,
                                     IShoppingCartService shoppingCartService,
                                     IHttpContextHelper httpContextHelper,
                                     IFileUploadHelper fileUploadHelper)
        {
            _mobileSuitService = mobileSuitService;
            _weaponService = weaponService;
            _webHostEnvironment = webHostEnvironment;
            _shoppingCartService = shoppingCartService;
            _httpContextHelper = httpContextHelper;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<IActionResult> Index(int page = 1, string searchMobileSuit = null)
        {
            MobileSuitListVM model = new()
            {
                MobileSuits = await _mobileSuitService.GetMobileSuits()
            };

            // Create url pagination parameters
            StringBuilder url = new();
            url.Append("/MobileSuits?page=:");

            // Determine if there is search criteria
            url.Append("&searchMobileSuit=");
            if (searchMobileSuit != null)
            {
                url.Append(searchMobileSuit);
            }

            // Apply filter to search results
            if (searchMobileSuit != null)
            {
                model.MobileSuits = model.MobileSuits.Where(m => m.Name.ToLower().Contains(searchMobileSuit.ToLower())).ToList();
            }

            // Get total number of items
            var count = model.MobileSuits.Count;

            // Initialize Pagination properties
            model.Pagination = new()
            {
                CurrentPage = page,
                ItemsPerPage = SD.MobileSuitsPerPage,
                TotalItems = count,
                UrlParam = url.ToString()
            };

            // Retrieve the items according to the Pagination
            model.MobileSuits = model.MobileSuits.Skip((page - 1) * SD.MobileSuitsPerPage)
                                                 .Take(SD.MobileSuitsPerPage)
                                                 .ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _mobileSuitService.GetMobileSuitDetails(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        // Add to Cart
        public async Task<IActionResult> Details(MobileSuit mobileSuit)
        {
            var response = await _shoppingCartService.AddMobileSuitCartItem(mobileSuit.Id);

            if (response.Success)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Error"] = response.Message;
                return Content("");
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Upsert(int? id)
        {
            MobileSuitUpsertVM model = new();

            // If inserting
            if (id == null)
            {
                return View(model);
            }

            // If updating
            else
            {
                model.MobileSuit = await _mobileSuitService.GetMobileSuitDetails(id.GetValueOrDefault());

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Upsert(MobileSuitUpsertVM model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            BaseCommandResponse response;

            // Get the web root path, and retrieve the file that has been uploaded
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = _httpContextHelper.GetUploadedFiles(this);

            // If an image file was uploaded
            if (files.Count > 0)
            {
                // Name the file with a Guid
                string fileName = Guid.NewGuid().ToString();
                // Navigate to the images path
                var uploads = Path.Combine(webRootPath, @"images\mobilesuits");
                // Get the extension of the uploaded file
                var extension = Path.GetExtension(files[0].FileName);

                // If editing
                if (model.MobileSuit.ImageUrl != null)
                {
                    // Remove the old image
                    var imagePath = Path.Combine(webRootPath, model.MobileSuit.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Upload the new image to static files
                _fileUploadHelper.UploadImageFile(files, uploads, fileName, extension);

                model.MobileSuit.ImageUrl = @"\images\mobilesuits\" + fileName + extension;
            }

            // Else, if the user did not upload a new image file
            else
            {
                // If we are editing, an image file for the product should already exist in the DB
                if (model.MobileSuit.Id != 0)
                {
                    // Retrieve the image stored in the DB
                    var mobileSuit = await _mobileSuitService.GetMobileSuitDetails(model.MobileSuit.Id);
                    model.MobileSuit.ImageUrl = mobileSuit.ImageUrl;
                }
                // If we are inserting, the user needs to upload an image, so throw an error
                else
                {
                    TempData["Error"] = "You must upload an Image File";
                    return View(model);
                }
            }

            // If inserting
            if (model.MobileSuit.Id == 0)
            {
                response = await _mobileSuitService.InsertMobileSuit(model.MobileSuit);
            }
            // If updating
            else
            {
                response = await _mobileSuitService.UpdateMobileSuit(model.MobileSuit);
            }

            if (response.Success)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response.Message;
                model.Errors = response.Errors;

                // If inserting
                if (model.MobileSuit.Id == 0)
                {
                    // Delete the image the user tried to upload
                    var imagePath = Path.Combine(webRootPath, model.MobileSuit.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                return View(model);
            }
        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            // Remove the image
            var mobileSuit = await _mobileSuitService.GetMobileSuitDetails(id);
            string webRootpath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootpath, mobileSuit.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            await _mobileSuitService.DeleteMobileSuit(id);

            TempData["Success"] = "Mobile Suit Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
