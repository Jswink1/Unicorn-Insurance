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
        Task<List<MobileSuitVM>> GetMobileSuits();
        Task<MobileSuitVM> GetMobileSuitDetails(int id);
        Task<BaseCommandResponse> InsertMobileSuit(MobileSuitVM mobileSuitVM);
        Task<BaseCommandResponse> UpdateMobileSuit(MobileSuitVM mobileSuitVM);
        Task DeleteMobileSuit(int id);
    }
}
