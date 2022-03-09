using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class UserMobileSuit
    {
        public int Id { get; set; }
        public MobileSuitVM MobileSuit { get; set; }
        public List<UserWeapon> EquippedWeapons { get; set; }
        public List<UserWeapon> AvailableWeapons { get; set; }
    }
}
