using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Weapon
{
    public interface IWeaponDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
