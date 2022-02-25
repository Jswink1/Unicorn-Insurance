using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Models
{
    public class MobileSuitCartItem
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public int MobileSuitId { get; set; }
        [ForeignKey("MobileSuitId")]
        public MobileSuit MobileSuit { get; set; }
        public decimal Price { get; set; }
    }
}
