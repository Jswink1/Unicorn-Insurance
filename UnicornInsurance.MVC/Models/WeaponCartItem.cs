using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class WeaponCartItem
    {
        public int Id { get; set; }
        public int WeaponId { get; set; }
        public WeaponVM Weapon { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
