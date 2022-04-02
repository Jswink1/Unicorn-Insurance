using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.OrderDetails;

namespace UnicornInsurance.Application.DTOs.Order
{
    public class InitializeOrderDTO
    {
        public List<CreateMobileSuitPurchaseDTO> MobileSuitPurchases { get; set; } = new();
        public List<CreateWeaponPurchaseDTO> WeaponPurchases { get; set; } = new();
    }
}
