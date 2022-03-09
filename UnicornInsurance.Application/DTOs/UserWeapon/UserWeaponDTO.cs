using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Weapon;

namespace UnicornInsurance.Application.DTOs.UserWeapon
{
    public class UserWeaponDTO
    {
        public int Id { get; set; }
        public WeaponDTO Weapon { get; set; }
        public int EquippedMobileSuitId { get; set; }
        public bool IsCustomWeapon { get; set; }
    }
}
