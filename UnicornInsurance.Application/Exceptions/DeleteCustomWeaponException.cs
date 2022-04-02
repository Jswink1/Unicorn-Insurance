using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class DeleteCustomWeaponException : ApplicationException
    {
        public DeleteCustomWeaponException() : base($"Custom Weapons must be deleted by updating the Mobile Suit they belong to.")
        {

        }
    }
}
