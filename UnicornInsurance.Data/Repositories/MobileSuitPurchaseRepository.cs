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
    }
}
