using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.ShoppingCart.Requests.Queries
{
    public class GetShoppingCartItemCountRequest : IRequest<ShoppingCartItemCountResponse>
    {
    }
}
