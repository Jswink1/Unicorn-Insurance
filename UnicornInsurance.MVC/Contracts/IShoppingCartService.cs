using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IShoppingCartService
    {
        Task<BaseCommandResponse> AddWeaponCartItem(int weaponId);
        Task<BaseCommandResponse> AddMobileSuitCartItem(int mobileSuitId);
        Task<List<WeaponCartItem>> GetAllWeaponCartItems();
        Task<List<MobileSuitCartItem>> GetAllMobileSuitCartItems();
        Task<ShoppingCartItemCountResponse> GetShoppingCartItemCount();
        Task DeleteMobileSuitCartItem(int cartItemId);
        Task DeleteWeaponCartItem(int cartItemId);
        Task IncreaseWeaponQuantity(int weaponId);
        Task DecreaseWeaponQuantity(int weaponId);
        Task ClearShoppingCart();

    }
}
