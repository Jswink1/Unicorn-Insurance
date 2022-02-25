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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class IncreaseWeaponCartQuantityHandler : IRequestHandler<IncreaseWeaponCartQuantityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IncreaseWeaponCartQuantityHandler(IUnitOfWork unitOfWork,
                                       IMapper mapper,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(IncreaseWeaponCartQuantityCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

            var weaponCartItem = await _unitOfWork.WeaponCartRepository.GetCartItem(userId, request.ItemId);

            if (weaponCartItem is null)
                throw new NotFoundException(nameof(weaponCartItem), request.ItemId);            

            weaponCartItem.Count++;
            weaponCartItem.Price = weaponCartItem.Weapon.Price * weaponCartItem.Count;
            await _unitOfWork.WeaponCartRepository.Update(weaponCartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
