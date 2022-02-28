using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderDetails
{
    public class CreateOrderDetailsDTO : IOrderDetailsDTO
    {
        public List<MobileSuitPurchaseDTO> MobileSuitPurchases { get; set; }
        public List<WeaponPurchaseDTO> WeaponPurchases { get; set; }
        public int OrderHeaderId { get; set; }
    }
}
