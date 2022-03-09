using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IUserItemService
    {
        Task<List<UserMobileSuit>> GetAllUserMobileSuits();
        Task<UserMobileSuit> GetUserMobileSuitDetails(int userMobileSuitId);
    }
}
