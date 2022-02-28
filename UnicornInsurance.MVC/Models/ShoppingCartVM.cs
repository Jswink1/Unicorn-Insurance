using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class ShoppingCartVM
    {
        public List<MobileSuitCartItem> MobileSuitCartItems { get; set; }
        public List<WeaponCartItem> WeaponCartItems { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
