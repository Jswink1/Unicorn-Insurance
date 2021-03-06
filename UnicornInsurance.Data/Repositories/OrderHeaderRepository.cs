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
    public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public OrderHeaderRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderHeader>> GetUserOrders(string userId)
        {
            var userOrders = await _dbContext.OrderHeaders.Where(o => o.ApplicationUserId == userId).ToListAsync();

            return userOrders;
        }

    }
}
