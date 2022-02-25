using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            await Initialize(ShoppingCartVM);

            return View(ShoppingCartVM);
        }

        public async Task<IActionResult> Checkout()
        {
            await Initialize(ShoppingCartVM);

            return View(ShoppingCartVM);
        }

        public async Task<IActionResult> IncreaseWeaponQuantity(int itemId)
        {
            await _shoppingCartService.IncreaseWeaponCartQuantity(itemId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseWeaponQuantity(int itemId)
        {
            await _shoppingCartService.DecreaseWeaponCartQuantity(itemId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteMobileSuitItem(int itemId)
        {
            await _shoppingCartService.DeleteMobileSuitCartItem(itemId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteWeaponItem(int itemId)
        {
            await _shoppingCartService.DeleteWeaponCartItem(itemId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ShoppingCartVM> Initialize(ShoppingCartVM shoppingCartVM)
        {
            ShoppingCartVM = new ShoppingCartVM()
            {
                MobileSuitCartItems = await _shoppingCartService.GetAllMobileSuitCartItems(),
                WeaponCartItems = await _shoppingCartService.GetAllWeaponCartItems()
            };

            if (ShoppingCartVM.MobileSuitCartItems.Count > 0)
            {
                foreach (var mobileSuitItem in ShoppingCartVM.MobileSuitCartItems)
                {
                    ShoppingCartVM.Total += mobileSuitItem.Price;
                }
            }

            if (ShoppingCartVM.WeaponCartItems.Count > 0)
            {
                foreach (var weaponSuitItem in ShoppingCartVM.WeaponCartItems)
                {
                    ShoppingCartVM.Total += weaponSuitItem.Price;
                }
            }

            return shoppingCartVM;
        }
    }
}
