using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Weapons.Handlers.Commands
{
    public class DeleteWeaponHandler : IRequestHandler<DeleteWeaponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWeaponHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteWeaponCommand request, CancellationToken cancellationToken)
        {
            var weapon = await _unitOfWork.WeaponRepository.Get(request.WeaponId);

            if (weapon is null)
                throw new NotFoundException(nameof(weapon), request.WeaponId);

            await _unitOfWork.WeaponRepository.Delete(weapon);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
