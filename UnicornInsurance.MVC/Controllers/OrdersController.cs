using Microsoft.AspNetCore.Authorization;
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
    [Authorize()]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        [BindProperty]
        public OrderDetailsVM OrderVM { get; set; }

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            OrderDetailsVM orderDetails = await _orderService.GetOrderDetails(id);            

            return View(orderDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Details")]
        // Stripe transaction for when a user payment gets rejected and they try again from the order details page
        public async Task<IActionResult> Details(string stripeToken)
        {
            var orderHeader = await _orderService.GetOrderHeader(OrderVM.OrderHeader.Id);

            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order Id : " + orderHeader.Id,
                    Source = stripeToken
                };

                CompleteOrderHeader completeOrderHeader = new() { OrderId = OrderVM.OrderHeader.Id };
                BaseCommandResponse completeOrderResponse = new();

                // Process the transaction
                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.Status.ToLower() == "succeeded")
                {
                    completeOrderHeader.TransactionSuccess = true;
                    completeOrderHeader.TransactionId = charge.Id;
                    completeOrderResponse = await _orderService.CompleteOrder(completeOrderHeader);
                }
                if (completeOrderResponse.Success == false)
                {
                    TempData["Error"] = completeOrderResponse.Message;
                }
            }
            return RedirectToAction("Details", "Orders", new { id = orderHeader.Id });
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetOrderList(string status)
        {
            var orders = (IEnumerable<OrderHeader>)await _orderService.GetOrders();

            switch (status)
            {
                case "pending":
                    orders = orders.Where(o => o.PaymentStatus == SD.PaymentStatusPending);
                    break;
                case "approved":
                    orders = orders.Where(o => o.PaymentStatus == SD.PaymentStatusApproved);
                    break;
                case "rejected":
                    orders = orders.Where(o => o.PaymentStatus == SD.PaymentStatusRejected);
                    break;
                default:
                    break;
            }

            return Json(new { data = orders });
        }

        #endregion
    }
}
