using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Contracts.Data
{
    public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
    {
        Task<List<OrderHeader>> GetUserOrders(string userId);
    }
}
