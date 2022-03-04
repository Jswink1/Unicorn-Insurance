using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

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
                Charge charge = new();
                CompleteOrderHeader completeOrderHeader = new() { OrderId = initialize.Id};
                BaseCommandResponse completeOrderResponse = new();
                try
                {
                    var service = new ChargeService();
                    charge = service.Create(options);
                }
                catch (Exception)
                {
                    completeOrderHeader.Success = false;
                    await _orderService.CompleteOrder(completeOrderHeader);
                    throw;
                }

                if (charge.Status.ToLower() == "succeeded")
                {
                    completeOrderHeader.Success = true;
                    completeOrderHeader.TransactionId = charge.Id;
                    completeOrderResponse = await _orderService.CompleteOrder(completeOrderHeader);
                }

                if (completeOrderResponse.Success == false)
                {
                    TempData["Error"] = completeOrderResponse.Message;
                }
            }
            else
            {
                TempData["Error"] = initialize.Message;
                return View(ShoppingCartVM);
            }

            return RedirectToAction("OrderConfirmation", "Cart", new { id = initialize.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        public async Task<IActionResult> IncreaseWeaponQuantity(int itemId)
        {
            await _shoppingCartService.IncreaseWeaponQuantity(itemId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseWeaponQuantity(int itemId)
        {
            await _shoppingCartService.DecreaseWeaponQuantity(itemId);

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
