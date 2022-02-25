using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Models;

namespace UnicornInsurance.Data.Repositories
{
    public class WeaponCartRepository : GenericRepository<WeaponCartItem>, IWeaponCartRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public WeaponCartRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeaponCartItem>> GetAllCartItems(string applicationUserId)
        {
            var cartItems = await _dbContext.WeaponCartItems.Where(x => x.ApplicationUserId == applicationUserId)
                                                            .Include(x => x.Weapon)
                                                            .ToListAsync();

            return cartItems;
        }

        public async Task<WeaponCartItem> GetCartItem(string applicationUserId, int weaponId)
        {
            var cartItem = await _dbContext.WeaponCartItems.Where(x => x.ApplicationUserId == applicationUserId)
                                                           .Include(x => x.Weapon)
                                                           .FirstOrDefaultAsync(x => x.WeaponId == weaponId);

            return cartItem;
        }
    }
}
