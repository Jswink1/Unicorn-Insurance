using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IWeaponService
    {
        Task<List<WeaponVM>> GetWeapons();
        Task<WeaponVM> GetWeaponDetails(int id);
        Task<BaseCommandResponse> InsertWeapon(WeaponVM weaponVM);
        Task<BaseCommandResponse> UpdateWeapon(WeaponVM weaponVM);
        Task DeleteWeapon(int id);
    }
}
