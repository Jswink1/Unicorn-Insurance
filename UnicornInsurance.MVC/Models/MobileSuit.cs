using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class MobileSuit
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }

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
        public string ImageUrl { get; set; }
        public CustomWeapon CustomWeapon { get; set; }        
    }
}
