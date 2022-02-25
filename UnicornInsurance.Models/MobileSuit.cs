using System;
using System.Collections.Generic;
using UnicornInsurance.Models.Common;

#nullable disable

namespace UnicornInsurance.Models
{
    public partial class MobileSuit : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string PowerOutput { get; set; }
        public string Armor { get; set; }
        public string ImageUrl { get; set; }

        public int? CustomWeaponId { get; set; }
        public virtual Weapon CustomWeapon { get; set; }        
    }
}
