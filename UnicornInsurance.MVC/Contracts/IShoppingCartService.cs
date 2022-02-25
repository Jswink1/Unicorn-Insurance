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
        // TODO: Rename "CreateCartItem" to "AddCartItem"
        Task<BaseCommandResponse> CreateWeaponCartItem(WeaponCartItem weaponCartItem);
        Task<BaseCommandResponse> CreateMobileSuitCartItem(MobileSuitCartItem mobileSuitCartItem);
        Task<List<WeaponCartItem>> GetAllWeaponCartItems();
        Task<List<MobileSuitCartItem>> GetAllMobileSuitCartItems();
        Task<ShoppingCartItemCountResponse> GetShoppingCartItemCount();
        Task DeleteMobileSuitCartItem(int itemId);
        Task DeleteWeaponCartItem(int itemId);
        Task IncreaseWeaponCartQuantity(int itemId);
        Task DecreaseWeaponCartQuantity(int itemId);

    }
}
