using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string UserEmail { get; set; }
        public string TransactionId { get; set; }
    }
}
