using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models.ViewModels
{
    public class WeaponUpsertVM
    {
        public Weapon Weapon { get; set; } = new();
        public ICollection<string> Errors { get; set; }
    }
}
