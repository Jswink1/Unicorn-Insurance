using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Contracts.Utility
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string email, string subject, string htmlMessage);
    }
}
