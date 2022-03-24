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
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;

namespace UnicornInsurance.Application.Features.UserItems.Handlers.Commands
{
    public class DeleteUserMobileSuitHandler : IRequestHandler<DeleteUserMobileSuitCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserMobileSuitHandler(IUnitOfWork unitOfWork,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteUserMobileSuitCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var userMobileSuit = await _unitOfWork.UserMobileSuitRepository.GetUserMobileSuit(request.UserMobileSuitId);

            if (userMobileSuit is null)
                throw new NotFoundException(nameof(userMobileSuit), request.UserMobileSuitId);
            if (userMobileSuit.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            var equippedWeapon = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitEquippedWeapon(request.UserMobileSuitId);

            if (equippedWeapon is not null)
                equippedWeapon.EquippedMobileSuitId = null;

            await _unitOfWork.UserMobileSuitRepository.Delete(userMobileSuit);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
