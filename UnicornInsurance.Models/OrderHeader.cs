using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        // Stripe Transaction ID
        public string TransactionId { get; set; }
    }
}
