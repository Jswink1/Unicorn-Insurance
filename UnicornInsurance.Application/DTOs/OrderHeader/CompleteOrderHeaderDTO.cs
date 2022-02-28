using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderHeader
{
    public class CompleteOrderHeaderDTO
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; }
        public int OrderId { get; set; }
    }
}
