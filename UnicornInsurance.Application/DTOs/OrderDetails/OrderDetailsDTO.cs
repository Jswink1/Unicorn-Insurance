using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Order;

namespace UnicornInsurance.Application.DTOs.OrderDetails
{
    public class OrderDetailsDTO
    {
        public List<MobileSuitPurchaseDTO> MobileSuitPurchases { get; set; } = new();
        public List<WeaponPurchaseDTO> WeaponPurchases { get; set; } = new();
        public OrderHeaderDTO OrderHeader { get; set; }
    }
}
