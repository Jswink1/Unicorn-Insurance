using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(LoginVM login);
        Task<bool> Register(RegisterVM registration);
        Task Logout();
        Task<bool> VerifyEmail(string token);
    }
}
