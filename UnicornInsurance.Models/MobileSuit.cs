using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnicornInsurance.Models.Common;

#nullable disable

namespace UnicornInsurance.Models
{
    public partial class MobileSuit : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Type { get; set; }

        [MaxLength(100)]
        public string Manufacturer { get; set; }

        [MaxLength(100)]
        public string Height { get; set; }

        [MaxLength(100)]
        public string Weight { get; set; }

        [MaxLength(100)]
        public string PowerOutput { get; set; }

        [MaxLength(100)]
        public string Armor { get; set; }

        [MaxLength(2000)]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public int? CustomWeaponId { get; set; }
        public virtual Weapon CustomWeapon { get; set; }        
    }
}
