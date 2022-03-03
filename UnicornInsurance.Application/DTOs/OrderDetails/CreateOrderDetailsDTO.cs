using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderDetails
{
    public class CreateOrderDetailsDTO
    {
        public List<CreateMobileSuitPurchaseDTO> MobileSuitPurchases { get; set; }
        public List<CreateWeaponPurchaseDTO> WeaponPurchases { get; set; }
        public int OrderHeaderId { get; set; }
    }
}
