using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Weapon;

namespace UnicornInsurance.Application.DTOs.MobileSuit
{
    public class CreateMobileSuitDTO : IMobileSuitDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string PowerOutput { get; set; }
        public string Armor { get; set; }
        public string ImageUrl { get; set; }
        public CustomWeaponDTO CustomWeapon { get; set; }
    }
}
