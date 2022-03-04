using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands
{
    public class CreateMobileSuitCartItemCommand : IRequest<BaseCommandResponse>
    {
        public CreateMobileSuitCartItemDTO MobileSuitCartItem { get; set; }
    }
}
