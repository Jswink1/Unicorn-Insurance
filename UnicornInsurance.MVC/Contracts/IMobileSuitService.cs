using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IMobileSuitService
    {
        Task<List<MobileSuit>> GetMobileSuits();
        Task<MobileSuit> GetMobileSuitDetails(int id);
        Task<BaseCommandResponse> InsertMobileSuit(MobileSuit mobileSuitVM);
        Task<BaseCommandResponse> UpdateMobileSuit(MobileSuit mobileSuitVM);
        Task DeleteMobileSuit(int id);
    }
}
