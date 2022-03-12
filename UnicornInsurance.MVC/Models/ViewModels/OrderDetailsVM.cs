using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class OrderDetailsVM
    {
        public List<MobileSuitCartItem> MobileSuitPurchases { get; set; } = new();
        public List<WeaponCartItem> WeaponPurchases { get; set; } = new();
        public OrderHeader OrderHeader { get; set; }
    }
}
