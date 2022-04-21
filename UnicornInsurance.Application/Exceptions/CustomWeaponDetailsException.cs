using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class CustomWeaponDetailsException : ApplicationException
    {
        public CustomWeaponDetailsException() : base($"To view the details of a Custom Weapon, you must view the details of the Mobile Suit it is attached to")
        {

        }
    }
}
