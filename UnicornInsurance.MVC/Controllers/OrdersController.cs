using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

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

            Console.WriteLine("sdfs");

            return View(orderDetails);
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
