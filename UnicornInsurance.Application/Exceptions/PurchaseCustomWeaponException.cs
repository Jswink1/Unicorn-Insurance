using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class PurchaseCustomWeaponException : ApplicationException
    {
        public PurchaseCustomWeaponException() : base($"Custom Weapons can only be purchased attached to the Mobile Suit they belong to")
        {

        }
    }
}
