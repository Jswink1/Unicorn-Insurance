using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class ShoppingCartVM
    {
        public List<MobileSuitCartItem> MobileSuitCartItems { get; set; } = new();
        public List<WeaponCartItem> WeaponCartItems { get; set; } = new();
        public OrderHeader OrderHeader { get; set; } = new();
    }
}
