using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class UserWeapon
    {
        public int Id { get; set; }
        public WeaponVM Weapon { get; set; }
        public int EquippedMobileSuitId { get; set; }
        public bool IsCustomWeapon { get; set; }
    }
}
