﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Controllers
{
    public class MobileSuitsController : Controller
    {
        private readonly IMobileSuitService _mobileSuitService;
        private readonly IWeaponService _weaponService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShoppingCartService _shoppingCartService;

        public MobileSuitsController(IMobileSuitService mobileSuitService,
                                     IWeaponService weaponService,
                                     IWebHostEnvironment webHostEnvironment,
                                     IShoppingCartService shoppingCartService)
        {
            _mobileSuitService = mobileSuitService;
            _weaponService = weaponService;
            _webHostEnvironment = webHostEnvironment;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _mobileSuitService.GetMobileSuits();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _mobileSuitService.GetMobileSuitDetails(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Add to Cart
        public async Task<IActionResult> Details(MobileSuitVM mobileSuitVM)
        {
            var response = await _shoppingCartService.AddMobileSuitCartItem(new MobileSuitCartItem
            {
                MobileSuitId = mobileSuitVM.Id,
                Price = mobileSuitVM.Price
            });

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

        public async Task<IActionResult> Upsert(int? id)
        {
            MobileSuitVM model;

            // If inserting
            if (id == null)
            {
                model = new MobileSuitVM();
                return View(model);
            }

            // If updating
            else
            {
                model = await _mobileSuitService.GetMobileSuitDetails(id.GetValueOrDefault());

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MobileSuitVM mobileSuitVM)
        {
            BaseCommandResponse response;

            // Get the web root path, and retrieve the file that has been uploaded
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            // If an image file was uploaded
            if (files.Count > 0)
            {
                // Name the file with a Guid
                string fileName = Guid.NewGuid().ToString();
                // Navigate to the images path
                var uploads = Path.Combine(webRootPath, @"images\mobilesuits");
                // Get the extension of the uploaded file
                var extension = Path.GetExtension(files[0].FileName);

                // If we are editing
                if (mobileSuitVM.ImageUrl != null)
                {
                    // Remove the old image
                    var imagePath = Path.Combine(webRootPath, mobileSuitVM.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Upload the new image to static files
                using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }
                mobileSuitVM.ImageUrl = @"\images\mobilesuits\" + fileName + extension;
            }

            // Else, if the user did not upload a new image file
            else
            {
                // If we are editing, an image file for the product should already exist in the DB
                if (mobileSuitVM.Id != 0)
                {
                    // Retrieve the image stored in the DB
                    var weapon = await _mobileSuitService.GetMobileSuitDetails(mobileSuitVM.Id);
                    mobileSuitVM.ImageUrl = weapon.ImageUrl;
                }
                // If we are inserting, the user needs to upload an image, so throw an error
                else
                {
                    TempData["Error"] = "You must upload an Image File";
                    return View(mobileSuitVM);
                }
            }

            // TODO: maybe refactor this logic and put it into the server-side, instead of client-side
            // If CustomWeapon input fields are empty
            if (String.IsNullOrWhiteSpace(mobileSuitVM.CustomWeapon.Name) ||
                String.IsNullOrWhiteSpace(mobileSuitVM.CustomWeapon.Description) ||
                mobileSuitVM.CustomWeapon.Price == 0)
            {
                // Set CustomWeapon to null so that an error is not thrown
                if (mobileSuitVM.CustomWeapon.Id == 0)
                {
                    mobileSuitVM.CustomWeapon = null;
                }
                // If there was a CustomWeapon initially, but the user is trying to remove it, delete the weapon and set the CustomWeapon to null
                else if (mobileSuitVM.CustomWeapon.Id != 0)
                {
                    await _weaponService.DeleteWeapon(mobileSuitVM.CustomWeapon.Id);
                    mobileSuitVM.CustomWeapon = null;
                }
            }

            // If inserting
            if (mobileSuitVM.Id == 0)
            {
                response = await _mobileSuitService.InsertMobileSuit(mobileSuitVM);
            }
            // If updating
            else
            {
                response = await _mobileSuitService.UpdateMobileSuit(mobileSuitVM);
            }

            if (response.Success)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response.Message;
                mobileSuitVM.Errors = response.Errors;
                return View(mobileSuitVM);
            }
        }

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
