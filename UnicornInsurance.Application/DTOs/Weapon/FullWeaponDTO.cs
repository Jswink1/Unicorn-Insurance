using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Common;

namespace UnicornInsurance.Application.DTOs.Weapon
{
    public class FullWeaponDTO : BaseDTO, IWeaponDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsCustomWeapon { get; set; }
    }
}
