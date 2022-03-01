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
        Task<BaseCommandResponse> InitializeOrder(OrderHeader orderHeader);
        Task<BaseCommandResponse> CreateOrderDetails(OrderDetails orderDetails);
        Task<BaseCommandResponse> CompleteOrder(CompleteOrderHeader orderHeaderCompletion);        
    }
}
