using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderHeader>> GetOrders();
        Task<BaseCommandResponse> InitializeOrder(ShoppingCartVM shoppingCart);
        Task<BaseCommandResponse> CompleteOrder(CompleteOrderHeader orderHeaderCompletion);
        Task<OrderDetailsVM> GetOrderDetails(int orderId);
        Task<OrderHeader> GetOrderHeader(int orderId);
    }
}
