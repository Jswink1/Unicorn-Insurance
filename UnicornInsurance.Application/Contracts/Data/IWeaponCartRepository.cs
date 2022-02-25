using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Contracts.Data
{
    public interface IWeaponCartRepository : IGenericRepository<WeaponCartItem>
    {
        Task<WeaponCartItem> GetCartItem(string applicationUserId, int weaponId);
        Task<List<WeaponCartItem>> GetAllCartItems(string applicationUserId);
    }
}
