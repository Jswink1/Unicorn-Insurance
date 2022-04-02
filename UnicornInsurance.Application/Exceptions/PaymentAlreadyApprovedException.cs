using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class PaymentAlreadyApprovedException : ApplicationException
    {
        public PaymentAlreadyApprovedException() : base($"Order has already been completed")
        {

        }
    }
}
