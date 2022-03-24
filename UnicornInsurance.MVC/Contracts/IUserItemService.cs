using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IUserItemService
    {
        Task<List<UserMobileSuit>> GetAllUserMobileSuits();
        Task<UserMobileSuit> GetUserMobileSuitDetails(int userMobileSuitId);
        Task<BaseCommandResponse> EquipWeapon(int selectedWeaponId, int userMobileSuitId);
        Task UnequipWeapon(int userMobileSuitId);
        Task<BaseCommandResponse> PurchaseInsurance(int userMobileSuitId, string selectedInsurance);
        Task DeleteUserMobileSuit(int userMobileSuitId);
    }
}
