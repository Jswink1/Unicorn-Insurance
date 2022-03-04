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
        Task<BaseCommandResponse> AddWeaponCartItem(WeaponCartItem weaponCartItem);
        Task<BaseCommandResponse> AddMobileSuitCartItem(MobileSuitCartItem mobileSuitCartItem);
        Task<List<WeaponCartItem>> GetAllWeaponCartItems();
        Task<List<MobileSuitCartItem>> GetAllMobileSuitCartItems();
        Task<ShoppingCartItemCountResponse> GetShoppingCartItemCount();
        Task DeleteMobileSuitCartItem(int itemId);
        Task DeleteWeaponCartItem(int itemId);
        Task IncreaseWeaponQuantity(int itemId);
        Task DecreaseWeaponQuantity(int itemId);
        Task ClearShoppingCart();

    }
}
