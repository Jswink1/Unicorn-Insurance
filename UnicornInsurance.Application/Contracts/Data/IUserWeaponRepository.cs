using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Contracts.Data
{
    public interface IUserWeaponRepository : IGenericRepository<UserWeapon>
    {
        Task CreateUserWeapon(string userId, WeaponPurchase weapon);
        Task<List<UserWeapon>> GetUserMobileSuitEquippedWeapons(int userMobileSuitId);
        Task<List<UserWeapon>> GetAvailableUserWeapons(string userId);
    }
}
