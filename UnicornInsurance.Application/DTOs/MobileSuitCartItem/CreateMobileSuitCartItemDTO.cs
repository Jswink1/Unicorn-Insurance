using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.MobileSuitCartItem
{
    public class CreateMobileSuitCartItemDTO : IMobileSuitCartItemDTO
    {
        public int MobileSuitId { get; set; }
        public decimal Price { get; set; }
    }
}
