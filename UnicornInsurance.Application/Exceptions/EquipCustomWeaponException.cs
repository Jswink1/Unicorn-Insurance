using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class EquipCustomWeaponException : ApplicationException
    {
        public EquipCustomWeaponException() : base($"Custom Weapons can not be separated from their original Mobile Suit")
        {

        }
    }
}
