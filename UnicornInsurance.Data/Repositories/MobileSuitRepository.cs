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
    public class MobileSuitRepository : GenericRepository<MobileSuit>, IMobileSuitRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public MobileSuitRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MobileSuit> GetFullMobileSuitDetails(int id)
        {
            var mobileSuit = await _dbContext.MobileSuits.Where(m => m.Id == id).Include(m => m.CustomWeapon).FirstOrDefaultAsync();

            return mobileSuit;
        }
    }
}
