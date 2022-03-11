using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models.ViewModels
{
    public class WeaponListVM
    {
        public List<Weapon> Weapons { get; set; }
        public Pagination Pagination { get; set; }
    }
}
