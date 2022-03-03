﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.OrderHeader;

namespace UnicornInsurance.Application.Features.Orders.Requests.Queries
{
    public class GetOrderHeaderRequest : IRequest<OrderHeaderDTO>
    {
        public int OrderId { get; set; }
    }
}