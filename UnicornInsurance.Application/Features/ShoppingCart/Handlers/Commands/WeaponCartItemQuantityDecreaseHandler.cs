using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class WeaponCartItemQuantityDecreaseHandler : IRequestHandler<WeaponCartItemQuantityDecreaseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeaponCartItemQuantityDecreaseHandler(IUnitOfWork unitOfWork,
                                                     IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(WeaponCartItemQuantityDecreaseCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var weaponCartItem = await _unitOfWork.WeaponCartRepository.GetCartItem(userId, request.WeaponId);

            if (weaponCartItem is null)
                throw new NotFoundException("Shopping Cart Item", request.WeaponId);
            if (weaponCartItem.Count <= 1)
                throw new DecreaseItemQuantityException();

            weaponCartItem.Count--;
            weaponCartItem.Price = weaponCartItem.Weapon.Price * weaponCartItem.Count;
            await _unitOfWork.WeaponCartRepository.Update(weaponCartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
