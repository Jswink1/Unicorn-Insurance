using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderDetails
{
    public class WeaponPurchaseDTO
    {
        public int WeaponId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
