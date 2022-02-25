using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class UpdateWeaponException : ApplicationException
    {
        public UpdateWeaponException() : base($"Can not update Custom Weapons through this method. Custom Weapons must be updated with their Mobile Suit.")
        {

        }
    }
}
