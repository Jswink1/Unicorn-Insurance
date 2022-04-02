using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Order
{
    public class CompleteOrderDTO
    {
        public int OrderId { get; set; }
        public bool TransactionSuccess { get; set; }
        public string TransactionId { get; set; }
    }
}
