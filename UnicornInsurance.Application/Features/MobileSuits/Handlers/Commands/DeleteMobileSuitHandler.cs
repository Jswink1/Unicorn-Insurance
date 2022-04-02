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
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands
{
    public class DeleteMobileSuitHandler : IRequestHandler<DeleteMobileSuitCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMobileSuitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteMobileSuitCommand request, CancellationToken cancellationToken)
        {
            var mobileSuit = await _unitOfWork.MobileSuitRepository.GetFullMobileSuitDetails(request.MobileSuitId);

            if (mobileSuit is null)
                throw new NotFoundException(nameof(mobileSuit), request.MobileSuitId);

            if (mobileSuit.CustomWeapon is not null)
                await _unitOfWork.WeaponRepository.Delete(mobileSuit.CustomWeapon);

            await _unitOfWork.MobileSuitRepository.Delete(mobileSuit);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
