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
    public class MobileSuitCartRepository : GenericRepository<MobileSuitCartItem>, IMobileSuitCartRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public MobileSuitCartRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CartItemExists(string applicationUserId, int mobileSuitId)
        {
            var cartItem = await _dbContext.MobileSuitCartItems.Where(x => x.ApplicationUserId == applicationUserId)
                                                       .FirstOrDefaultAsync(x => x.MobileSuitId == mobileSuitId);

            if (cartItem is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<MobileSuitCartItem>> GetAllCartItems(string applicationUserId)
        {
            var cartItems = await _dbContext.MobileSuitCartItems.Where(x => x.ApplicationUserId == applicationUserId)
                                                                .Include(x => x.MobileSuit)
                                                                .ToListAsync();

            return cartItems;
        }
    }
}
