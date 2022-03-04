using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class DeleteWeaponCartItemHandler : IRequestHandler<DeleteWeaponCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWeaponCartItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteWeaponCartItemCommand request, CancellationToken cancellationToken)
        {
            var weaponCartItem = await _unitOfWork.WeaponCartRepository.Get(request.Id);

            if (weaponCartItem is null)
                throw new NotFoundException(nameof(weaponCartItem), request.Id);

            await _unitOfWork.WeaponCartRepository.Delete(weaponCartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
