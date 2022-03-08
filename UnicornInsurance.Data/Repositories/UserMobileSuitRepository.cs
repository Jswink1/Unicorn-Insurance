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
    public class UserMobileSuitRepository : GenericRepository<UserMobileSuit>, IUserMobileSuitRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public UserMobileSuitRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserMobileSuit(string userId, MobileSuitPurchase mobileSuit)
        {
            UserMobileSuit userMobileSuit = new()
            {
                ApplicationUserId = userId,
                MobileSuitId = mobileSuit.MobileSuitId
            };

            await _dbContext.UserMobileSuits.AddAsync(userMobileSuit);
            await _dbContext.SaveChangesAsync();

            if (mobileSuit.MobileSuit.CustomWeaponId is not null)
            {
                UserWeapon customWeapon = new()
                {
                    ApplicationUserId = userId,
                    WeaponId = (int)mobileSuit.MobileSuit.CustomWeaponId,
                    IsCustomWeapon = true,
                    EquippedMobileSuitId = userMobileSuit.Id
                };
                
                await _dbContext.UserWeapons.AddAsync(customWeapon);
            }

            return;
        }

        public async Task<List<UserMobileSuit>> GetAllUserMobileSuits(string applicationUserId)
        {
            var userMobileSuits = await _dbContext.UserMobileSuits.Where(x => x.ApplicationUserId == applicationUserId)
                                                                .Include(x => x.MobileSuit)
                                                                .ToListAsync();

            return userMobileSuits;
        }
    }
}
