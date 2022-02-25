using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.WeaponCartItem
{
    public interface IWeaponCartItemDTO
    {
        public int WeaponId { get; set; }
        public decimal Price { get; set; }
    }
}
