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
    public class MobileSuitPurchaseRepository : GenericRepository<MobileSuitPurchase>, IMobileSuitPurchaseRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public MobileSuitPurchaseRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MobileSuitPurchase>> GetMobileSuitPurchasesForOrder(int orderId)
        {
            var mobileSuitPurchases = await _dbContext.MobileSuitPurchases.Where(p => p.OrderId == orderId)
                                                                          .Include(p => p.MobileSuit)                
                                                                          .ToListAsync();

            return mobileSuitPurchases;
        }
    }
}
