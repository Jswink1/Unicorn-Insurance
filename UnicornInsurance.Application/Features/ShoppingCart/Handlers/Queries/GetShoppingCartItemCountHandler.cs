using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Queries
{
    public class GetShoppingCartItemCountHandler : IRequestHandler<GetShoppingCartItemCountRequest, ShoppingCartItemCountResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetShoppingCartItemCountHandler(IUnitOfWork unitOfWork,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingCartItemCountResponse> Handle(GetShoppingCartItemCountRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var mobileSuitCartItemsCount = await _unitOfWork.MobileSuitCartRepository.GetAllCartItems(userId);
            var weaponCartItemsCount = await _unitOfWork.WeaponCartRepository.GetAllCartItems(userId);

            var response = new ShoppingCartItemCountResponse
            {
                ShoppingCartItemCount = mobileSuitCartItemsCount.Count() + weaponCartItemsCount.Count()
            };

            return response;
        }
    }
}
