using Microsoft.AspNetCore.Hosting;
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
    public class WeaponsController : Controller
    {
        private readonly IWeaponService _weaponService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShoppingCartService _shoppingCartService;

        public WeaponsController(IWeaponService weaponService,
                                 IWebHostEnvironment webHostEnvironment,
                                 IShoppingCartService shoppingCartService)
        {
            _weaponService = weaponService;
            _webHostEnvironment = webHostEnvironment;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _weaponService.GetWeapons();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _weaponService.GetWeaponDetails(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Add to Cart
        public async Task<IActionResult> Details(WeaponVM weaponVM)
        {
            var response = await _shoppingCartService.AddWeaponCartItem(new WeaponCartItem
            {
                WeaponId = weaponVM.Id,
                Price = weaponVM.Price
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
            WeaponVM model;

            // If inserting
            if (id == null)
            {
                model = new WeaponVM();
                return View(model);
            }

            // If updating
            else
            {
                model = await _weaponService.GetWeaponDetails(id.GetValueOrDefault());

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(WeaponVM weaponVM)
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
                var uploads = Path.Combine(webRootPath, @"images\weapons");
                // Get the extension of the uploaded file
                var extension = Path.GetExtension(files[0].FileName);

                // If user is editing
                if (weaponVM.ImageUrl != null)
                {
                    // Remove the old image
                    var imagePath = Path.Combine(webRootPath, weaponVM.ImageUrl.TrimStart('\\'));
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
                weaponVM.ImageUrl = @"\images\weapons\" + fileName + extension;
            }

            // Else, if the user did not upload a new image file
            else
            {
                // If user is editing, an image file for the product should already exist in the DB
                if (weaponVM.Id != 0)
                {
                    // Retrieve the image stored in the DB
                    var weapon = await _weaponService.GetWeaponDetails(weaponVM.Id);
                    weaponVM.ImageUrl = weapon.ImageUrl;
                }
                // If user is inserting, the user needs to upload an image, so throw an error
                else
                {
                    TempData["Error"] = "You must upload an Image File";
                    return View(weaponVM);
                }
            }


            // If inserting
            if (weaponVM.Id == 0)
            {
                response = await _weaponService.InsertWeapon(weaponVM);                                        
            }
            // If updating
            else
            {
                response = await _weaponService.UpdateWeapon(weaponVM);
            }

            if (response.Success)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response.Message;
                weaponVM.Errors = response.Errors;
                return View(weaponVM);
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
