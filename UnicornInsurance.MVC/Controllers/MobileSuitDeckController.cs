using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Models.ViewModels;

namespace UnicornInsurance.MVC.Controllers
{
    public class MobileSuitDeckController : Controller
    {
        private readonly IUserItemService _userItemService;

        public MobileSuitDeckController(IUserItemService userItemService)
        {
            _userItemService = userItemService;
        }

        public async Task<IActionResult> Index()
        {
            var userMobileSuits = await _userItemService.GetAllUserMobileSuits();

            return View(userMobileSuits);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userMobileSuitDetails = await _userItemService.GetUserMobileSuitDetails(id);

            userMobileSuitDetails.AvailableWeaponsList = new SelectList(userMobileSuitDetails.AvailableWeapons, "Id", "Weapon.Name");

            return View(userMobileSuitDetails);
        }

        [HttpPost]
        [ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(UserMobileSuit userMobileSuit)
        {
            await _userItemService.EquipWeapon(userMobileSuit.SelectedWeaponId, userMobileSuit.Id);

            return RedirectToAction("Details", "MobileSuitDeck", new { id = userMobileSuit.Id });
        }

        public async Task<IActionResult> UnequipWeapon(int userMobileSuitId)
        {
            await _userItemService.UnequipWeapon(userMobileSuitId);

            return RedirectToAction("Details", "MobileSuitDeck", new { id = userMobileSuitId });
        }

        public async Task<IActionResult> Insurance(int id)
        {
            InsuranceVM model = new()
            {
                UserMobileSuit = await _userItemService.GetUserMobileSuitDetails(id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insurance(InsuranceVM model)
        {
            await _userItemService.PurchaseInsurance(model.UserMobileSuit.Id, model.SelectedInsurance);

            return RedirectToAction("Details", "MobileSuitDeck", new { id = model.UserMobileSuit.Id });
        }
    }
}
