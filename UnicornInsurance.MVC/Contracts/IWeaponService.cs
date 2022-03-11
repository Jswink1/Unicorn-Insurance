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
        Task<List<Weapon>> GetWeapons();
        Task<Weapon> GetWeaponDetails(int id);
        Task<BaseCommandResponse> InsertWeapon(Weapon weaponVM);
        Task<BaseCommandResponse> UpdateWeapon(Weapon weaponVM);
        Task DeleteWeapon(int id);
    }
}
