using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class OrderDetails
    {
        public List<MobileSuitCartItem> MobileSuitPurchases { get; set; }
        public List<WeaponCartItem> WeaponPurchases { get; set; }
        public int OrderHeaderId { get; set; }
    }
}
