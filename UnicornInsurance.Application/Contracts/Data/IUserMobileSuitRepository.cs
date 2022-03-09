using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Contracts.Data
{
    public interface IUserMobileSuitRepository : IGenericRepository<UserMobileSuit>
    {
        Task CreateUserMobileSuit(string userId, MobileSuitPurchase mobileSuit);
        Task<List<UserMobileSuit>> GetAllUserMobileSuits(string applicationUserId);
        Task<UserMobileSuit> GetUserMobileSuit(int id);
    }
}
