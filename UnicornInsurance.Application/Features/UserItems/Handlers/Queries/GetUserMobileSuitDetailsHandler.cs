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
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;

namespace UnicornInsurance.Application.Features.UserItems.Handlers.Queries
{
    public class GetUserMobileSuitDetailsHandler : IRequestHandler<GetUserMobileSuitDetailsRequest, FullUserMobileSuitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserMobileSuitDetailsHandler(IUnitOfWork unitOfWork, 
                                               IMapper mapper,
                                               IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FullUserMobileSuitDTO> Handle(GetUserMobileSuitDetailsRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var userMobileSuit = await _unitOfWork.UserMobileSuitRepository.GetUserMobileSuit(request.UserMobileSuitId);

            if (userMobileSuit is null)
                throw new NotFoundException(nameof(userMobileSuit), request.UserMobileSuitId);
            if (userMobileSuit.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            var equippedWeapon = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitEquippedWeapon(userMobileSuit.Id);
            var customWeapon = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitCustomWeapon(userMobileSuit.Id);
            var availableWeapons = await _unitOfWork.UserWeaponRepository.GetAvailableUserWeapons(userId);
            
            // Remove duplicate of EquippedWeapon from the list of AvailableWeapons
            if (equippedWeapon is not null)
            {
                if (availableWeapons.Any(w => w.WeaponId == equippedWeapon.WeaponId))
                {
                    availableWeapons.RemoveAll(w => w.WeaponId == equippedWeapon.WeaponId);
                }
            }

            FullUserMobileSuitDTO userMobileSuitDTO = new()
            {
                Id = userMobileSuit.Id,
                MobileSuit = _mapper.Map<MobileSuitDTO>(userMobileSuit.MobileSuit),
                EquippedWeapon = _mapper.Map<UserWeaponDTO>(equippedWeapon),
                CustomWeapon = _mapper.Map<UserWeaponDTO>(customWeapon),
                AvailableWeapons = _mapper.Map<List<UserWeaponDTO>>(availableWeapons),
                EndOfCoverage = userMobileSuit.EndOfCoverage,
                InsurancePlan = userMobileSuit.InsurancePlan,
                IsDamaged = userMobileSuit.IsDamaged
            };

            return userMobileSuitDTO;
        }
    }
}
