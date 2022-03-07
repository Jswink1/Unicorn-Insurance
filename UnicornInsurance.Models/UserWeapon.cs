using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Models
{
    public class UserWeapon
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsCustomWeapon { get; set; }

        public int WeaponId { get; set; }
        [ForeignKey("WeaponId")]
        public Weapon Weapon { get; set; }

        public int? EquippedMobileSuitId { get; set; }
        [ForeignKey("EquippedMobileSuitId")]
        public UserMobileSuit UserMobileSuit { get; set; }
    }
}
