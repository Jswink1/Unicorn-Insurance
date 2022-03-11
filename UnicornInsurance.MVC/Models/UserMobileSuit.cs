using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class UserMobileSuit
    {
        public int Id { get; set; }
        public MobileSuit MobileSuit { get; set; }
        public UserWeapon EquippedWeapon { get; set; }
        public UserWeapon CustomWeapon { get; set; }
        public List<UserWeapon> AvailableWeapons { get; set; }
        public SelectList AvailableWeaponsList { get; set; }
        public int SelectedWeaponId { get; set; }
    }
}
