using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.DTOs.WeaponCartItem;

namespace UnicornInsurance.Application.DTOs.OrderDetails
{
    public interface IOrderDetailsDTO
    {
        public List<MobileSuitPurchaseDTO> MobileSuitPurchases { get; set; }
        public List<WeaponPurchaseDTO> WeaponPurchases { get; set; }
        public int OrderHeaderId { get; set; }
    }
}
