using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderHeader
{
    public class InitializeOrderHeaderDTO : IOrderHeaderDTO
    {
        public decimal OrderTotal { get; set; }
    }
}
