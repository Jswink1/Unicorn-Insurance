using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Contracts.Data
{
    public interface IMobileSuitCartRepository : IGenericRepository<MobileSuitCartItem>
    {
        Task<bool> CartItemExists(string applicationUserId, int mobileSuitId);
        Task<List<MobileSuitCartItem>> GetAllCartItems(string applicationUserId);
    }
}
