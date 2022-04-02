using MediatR;
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands
{
    public class AddMobileSuitCartItemCommand : IRequest<BaseCommandResponse>
    {
        public int MobileSuitId { get; set; }
    }
}
