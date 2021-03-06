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
    public class WeaponPurchaseRepository : GenericRepository<WeaponPurchase>, IWeaponPurchaseRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public WeaponPurchaseRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeaponPurchase>> GetWeaponPurchasesForOrder(int orderId)
        {
            var weaponPurchases = await _dbContext.WeaponPurchases.Where(p => p.OrderId == orderId)
                                                                  .Include(p => p.Weapon)
                                                                  .ToListAsync();

            return weaponPurchases;
        }
    }
}
