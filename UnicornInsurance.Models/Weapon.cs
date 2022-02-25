using System;
using System.Collections.Generic;
using UnicornInsurance.Models.Common;

#nullable disable

namespace UnicornInsurance.Models
{
    public partial class Weapon : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsCustomWeapon { get; set; }
        public string ImageUrl { get; set; }
    }
}
