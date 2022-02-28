using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IShoppingCartService shoppingCartService,
                              IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            await Initialize(ShoppingCartVM);

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string stripeToken)
        {
            await Initialize(ShoppingCartVM);            

            var initialize = await _orderService.InitializeOrder(ShoppingCartVM.OrderHeader);

            if (initialize.Success)
            {
                OrderDetails orderDetails = new()
                {
                    MobileSuitPurchases = ShoppingCartVM.MobileSuitCartItems,
                    WeaponPurchases = ShoppingCartVM.WeaponCartItems,
                    OrderHeaderId = initialize.Id
                };

                await _orderService.CreateOrderDetails(orderDetails);
                await _shoppingCartService.ClearShoppingCart();

                var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(ShoppingCartVM.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order Id : " + initialize.Id,
                    Source = stripeToken
                };

                // Process the transaction
                var service = new ChargeService();
                Charge charge = service.Create(options);

                CompleteOrderHeader completeOrderHeader = new();

                if (charge.BalanceTransactionId == null)
                {
                    completeOrderHeader.Success = false;
                }
                else
                {
                    completeOrderHeader.TransactionId = charge.Id;
                }
                if (charge.Status.ToLower() == "succeeded")
                {
                    completeOrderHeader.Success = true;
                }

                completeOrderHeader.OrderId = initialize.Id;
                var orderCompletion = await _orderService.CompleteOrder(completeOrderHeader);

                if (orderCompletion.Success == false)
                {
                    TempData["Error"] = orderCompletion.Message;
                }

            }
            else
            {
                TempData["Error"] = initialize.Message;
            }

            return RedirectToAction("Index", "Home");
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
                WeaponCartItems = await _shoppingCartService.GetAllWeaponCartItems(),
                OrderHeader = new()
            };

            if (ShoppingCartVM.MobileSuitCartItems.Count > 0)
            {
                foreach (var mobileSuitItem in ShoppingCartVM.MobileSuitCartItems)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += mobileSuitItem.Price;
                }
            }

            if (ShoppingCartVM.WeaponCartItems.Count > 0)
            {
                foreach (var weaponSuitItem in ShoppingCartVM.WeaponCartItems)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += weaponSuitItem.Price;
                }
            }

            return shoppingCartVM;
        }
    }
}
