using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Models;

namespace UnicornInsurance.Data.Repositories
{
    public class UserWeaponRepository : GenericRepository<UserWeapon>, IUserWeaponRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public UserWeaponRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserWeapon(string userId, WeaponPurchase weapon)
        {
            for (int i = 0; i < weapon.Count; i++)
            {
                UserWeapon userWeapon = new()
                {
                    ApplicationUserId = userId,
                    WeaponId = weapon.WeaponId,
                    IsCustomWeapon = false
                };

                await _dbContext.UserWeapons.AddAsync(userWeapon);                
            }

            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task<UserWeapon> GetUserMobileSuitEquippedWeapon(int userMobileSuitId)
        {
            var equippedWeapon = await _dbContext.UserWeapons.Where(w => w.EquippedMobileSuitId == userMobileSuitId)
                                                                    .Include(w => w.Weapon)
                                                                    .FirstOrDefaultAsync();

            return equippedWeapon;
        }

        public async Task<List<UserWeapon>> GetAvailableUserWeapons(string userId)
        {
            var availableWeapons = await _dbContext.UserWeapons.Where(w => w.ApplicationUserId == userId)
                                                               .Where(w => w.EquippedMobileSuitId == null)
                                                               .Include(w => w.Weapon)
                                                               .ToListAsync();

            availableWeapons = availableWeapons.GroupBy(w => w.WeaponId)
                                               .Select(g => g.First())
                                               .ToList();

            return availableWeapons;
        }

        public async Task EquipWeapon(int selectedWeaponId, int userMobileSuitId)
        {
            var selectedWeapon = _dbContext.UserWeapons.Where(w => w.Id == selectedWeaponId).FirstOrDefault();
            selectedWeapon.EquippedMobileSuitId = userMobileSuitId;
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
