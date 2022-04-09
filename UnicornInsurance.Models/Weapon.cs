using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnicornInsurance.Models.Common;

#nullable disable

namespace UnicornInsurance.Models
{
    public partial class Weapon : BaseModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required]
        public bool IsCustomWeapon { get; set; }

        [MaxLength(2000)]
        public string ImageUrl { get; set; }
    }
}
