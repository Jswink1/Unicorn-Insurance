using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.OrderHeader;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Orders.Requests.Commands
{
    public class InitializeOrderHeaderCommand : IRequest<BaseCommandResponse>
    {
        public InitializeOrderHeaderDTO OrderHeaderDTO { get; set; }
    }
}
