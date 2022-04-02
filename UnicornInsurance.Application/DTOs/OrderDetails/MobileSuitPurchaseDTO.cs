using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;

namespace UnicornInsurance.Application.DTOs.OrderDetails
{
    public class MobileSuitPurchaseDTO
    {
        public int Id { get; set; }
        public MobileSuitDTO MobileSuit { get; set; }
        public decimal Price { get; set; }
    }
}
