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

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class DeleteWeaponCartItemHandler : IRequestHandler<DeleteWeaponCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteWeaponCartItemHandler(IUnitOfWork unitOfWork,
                                           IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteWeaponCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var weaponCartItem = await _unitOfWork.WeaponCartRepository.Get(request.WeaponCartItemId);

            if (weaponCartItem is null)
                throw new NotFoundException(nameof(weaponCartItem), request.WeaponCartItemId);
            if (weaponCartItem.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            await _unitOfWork.WeaponCartRepository.Delete(weaponCartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
