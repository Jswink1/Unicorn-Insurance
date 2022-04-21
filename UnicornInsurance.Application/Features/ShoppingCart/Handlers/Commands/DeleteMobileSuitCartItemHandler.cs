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
    public class DeleteMobileSuitCartItemHandler : IRequestHandler<DeleteMobileSuitCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteMobileSuitCartItemHandler(IUnitOfWork unitOfWork,
                                               IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteMobileSuitCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var mobileSuitCartItem = await _unitOfWork.MobileSuitCartRepository.Get(request.MobileSuitCartItemId);

            if (mobileSuitCartItem is null)
                throw new NotFoundException("Shopping Cart Item", request.MobileSuitCartItemId);
            if (mobileSuitCartItem.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            await _unitOfWork.MobileSuitCartRepository.Delete(mobileSuitCartItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
