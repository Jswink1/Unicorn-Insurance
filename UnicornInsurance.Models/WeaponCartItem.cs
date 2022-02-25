using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Models
{
    public class WeaponCartItem
    {
        public WeaponCartItem()
        {
            Count = 1;
        }

        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public int WeaponId { get; set; }
        [ForeignKey("WeaponId")]
        public Weapon Weapon { get; set; }
        public decimal Price { get; set; }

        [Range(1, 100, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }
    }
}
