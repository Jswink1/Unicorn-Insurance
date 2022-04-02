using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class DecreaseItemQuantityException : ApplicationException
    {
        public DecreaseItemQuantityException() : base($"Can not decrease item quantity below 1. You must delete the item.")
        {

        }
    }
}
