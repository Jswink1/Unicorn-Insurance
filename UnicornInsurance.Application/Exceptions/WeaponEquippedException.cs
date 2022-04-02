using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class WeaponEquippedException : ApplicationException
    {
        public WeaponEquippedException() : base($"Weapon is already equipped to another Mobile Suit")
        {

        }
    }
}
