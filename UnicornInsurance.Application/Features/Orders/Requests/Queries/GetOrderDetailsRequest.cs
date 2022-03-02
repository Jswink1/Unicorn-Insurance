using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.OrderDetails;

namespace UnicornInsurance.Application.Features.Orders.Requests.Queries
{
    public class GetOrderDetailsRequest : IRequest<OrderDetailsDTO>
    {
        public int OrderId { get; set; }
    }
}
