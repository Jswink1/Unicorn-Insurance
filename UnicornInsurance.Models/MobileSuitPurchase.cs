using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Models
{
    public class MobileSuitPurchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderHeader OrderHeader { get; set; }

        // The product the user has ordered
        [Required]
        public int MobileSuitId { get; set; }
        [ForeignKey("MobileSuitId")]
        public MobileSuit MobileSuit { get; set; }

        public decimal Price { get; set; }
    }
}
