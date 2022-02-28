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
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class ClearShoppingCartHandler : IRequestHandler<ClearShoppingCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClearShoppingCartHandler(IUnitOfWork unitOfWork,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(ClearShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var mobileSuitCartItems = await _unitOfWork.MobileSuitCartRepository.GetAllCartItems(userId);
            foreach (var mobileSuitCartItem in mobileSuitCartItems)
            {
                await _unitOfWork.MobileSuitCartRepository.Delete(mobileSuitCartItem);
            }

            var weaponCartItems = await _unitOfWork.WeaponCartRepository.GetAllCartItems(userId);
            foreach (var weaponCartItem in weaponCartItems)
            {
                await _unitOfWork.WeaponCartRepository.Delete(weaponCartItem);
            }

            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
