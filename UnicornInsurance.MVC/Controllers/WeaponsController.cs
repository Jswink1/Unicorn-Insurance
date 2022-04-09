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
    public class WeaponsController : Controller
    {
        private readonly IWeaponService _weaponService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IFileUploadHelper _fileUploadHelper;

        public WeaponsController(IWeaponService weaponService,
                                 IWebHostEnvironment webHostEnvironment,
                                 IShoppingCartService shoppingCartService,
                                 IHttpContextHelper httpContextHelper,
                                 IFileUploadHelper fileUploadHelper)
        {
            _weaponService = weaponService;
            _webHostEnvironment = webHostEnvironment;
            _shoppingCartService = shoppingCartService;
            _httpContextHelper = httpContextHelper;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<IActionResult> Index(int page = 1, string searchWeapon = null)
        {
            WeaponListVM model = new()
            {
                Weapons = await _weaponService.GetWeapons()
            };

            // Create url pagination parameters
            StringBuilder url = new();
            url.Append("/Weapons?page=:");

            // Determine if there is search criteria
            url.Append("&searchWeapon=");
            if (searchWeapon != null)
            {
                url.Append(searchWeapon);
            }

            // Apply filter to search results
            if (searchWeapon != null)
            {
                model.Weapons = model.Weapons.Where(m => m.Name.ToLower().Contains(searchWeapon.ToLower())).ToList();
            }

            // Get total number of items
            var count = model.Weapons.Count;

            // Initialize Pagination properties
            model.Pagination = new()
            {
                CurrentPage = page,
                ItemsPerPage = SD.WeaponsPerPage,
                TotalItems = count,
                UrlParam = url.ToString()
            };

            // Retrieve the items according to the Pagination
            model.Weapons = model.Weapons.Skip((page - 1) * SD.WeaponsPerPage)
                                         .Take(SD.WeaponsPerPage)
                                         .ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _weaponService.GetWeaponDetails(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        // Add to Cart
        public async Task<IActionResult> Details(Weapon weapon)
        {
            var response = await _shoppingCartService.AddWeaponCartItem(weapon.Id);

            if (response.Success)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Error"] = response.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Upsert(int? id)
        {
            WeaponUpsertVM model = new();

            // If inserting
            if (id == null)
            {
                return View(model);
            }

            // If updating
            else
            {
                model.Weapon = await _weaponService.GetWeaponDetails(id.GetValueOrDefault());

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> Upsert(WeaponUpsertVM model)
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
                var uploads = Path.Combine(webRootPath, @"images\weapons");
                // Get the extension of the uploaded file
                var extension = Path.GetExtension(files[0].FileName);

                // If user is editing
                if (model.Weapon.ImageUrl != null)
                {
                    // Remove the old image
                    var imagePath = Path.Combine(webRootPath, model.Weapon.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Upload the new image to static files
                _fileUploadHelper.UploadImageFile(files, uploads, fileName, extension);

                model.Weapon.ImageUrl = @"\images\weapons\" + fileName + extension;
            }

            // Else, if the user did not upload a new image file
            else
            {
                // If user is editing, an image file for the product should already exist in the DB
                if (model.Weapon.Id != 0)
                {
                    // Retrieve the image stored in the DB
                    var weapon = await _weaponService.GetWeaponDetails(model.Weapon.Id);
                    model.Weapon.ImageUrl = weapon.ImageUrl;
                }
                // If user is inserting, the user needs to upload an image, so throw an error
                else
                {
                    TempData["Error"] = "You must upload an Image File";
                    return View(model);
                }
            }


            // If inserting
            if (model.Weapon.Id == 0)
            {
                response = await _weaponService.InsertWeapon(model.Weapon);                                        
            }
            // If updating
            else
            {
                response = await _weaponService.UpdateWeapon(model.Weapon);
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
                if (model.Weapon.Id == 0)
                {
                    // Delete the image the user tried to upload
                    var imagePath = Path.Combine(webRootPath, model.Weapon.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }                

                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            // Remove the image
            var weapon = await _weaponService.GetWeaponDetails(id);
            string webRootpath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootpath, weapon.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            await _weaponService.DeleteWeapon(id);

            TempData["Success"] = "Weapon Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
