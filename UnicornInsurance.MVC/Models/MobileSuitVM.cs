using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class MobileSuitVM
    {
        public int Id { get; set; }
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
        public WeaponVM CustomWeapon { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
