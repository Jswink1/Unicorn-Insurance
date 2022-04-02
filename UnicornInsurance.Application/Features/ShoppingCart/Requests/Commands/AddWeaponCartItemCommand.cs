using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands
{
    public class AddWeaponCartItemCommand : IRequest<BaseCommandResponse>
    {
        public int WeaponId { get; set; }
    }
}
