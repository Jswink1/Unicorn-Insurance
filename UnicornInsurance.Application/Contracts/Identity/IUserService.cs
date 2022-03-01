using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<IList<string>> GetUserRoles(string userId);
        Task<string> GetUserEmail(string userId);
    }
}
