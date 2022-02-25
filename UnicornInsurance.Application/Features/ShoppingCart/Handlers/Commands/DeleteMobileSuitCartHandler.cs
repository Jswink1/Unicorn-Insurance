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
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class DeleteMobileSuitCartHandler : IRequestHandler<DeleteMobileSuitCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMobileSuitCartHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteMobileSuitCartCommand request, CancellationToken cancellationToken)
        {
            var mobileSuitCartItem = await _unitOfWork.MobileSuitCartRepository.Get(request.Id);

            if (mobileSuitCartItem is null)
                throw new NotFoundException(nameof(mobileSuitCartItem), request.Id);

            await _unitOfWork.MobileSuitCartRepository.Delete(mobileSuitCartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
