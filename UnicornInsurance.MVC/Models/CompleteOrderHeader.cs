using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class CompleteOrderHeader
    {
        public bool TransactionSuccess { get; set; }
        public string TransactionId { get; set; }
        public int OrderId { get; set; }
    }
}
