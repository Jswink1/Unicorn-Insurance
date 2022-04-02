using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Weapon;

namespace UnicornInsurance.Application.DTOs.WeaponCartItem
{
    public class WeaponCartItemDTO
    {
        public int Id { get; set; }        
        public decimal Price { get; set; }
        public int Count { get; set; }
        public WeaponDTO Weapon { get; set; }
    }
}
